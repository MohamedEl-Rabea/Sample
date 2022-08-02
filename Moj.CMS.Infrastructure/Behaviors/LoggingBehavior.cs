using MediatR;
using Microsoft.AspNetCore.Http;
using Moj.CMS.Domain.Shared.Exceptions;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Runtime;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : RequestBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Log> _logRepository;
        private readonly IApplicationSession _applicationSession;

        public LoggingBehavior(IHttpContextAccessor httpContextAccessor, IRepository<Log> logRepository, IApplicationSession applicationSession)
        {
            _httpContextAccessor = httpContextAccessor;
            _logRepository = logRepository;
            _applicationSession = applicationSession;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var log = SetLoggingInfo(request);
            _applicationSession.RequestId = request.RequestId;
            _applicationSession.RequestName = request.RequestName;
            try
            {
                var result = await next();
                sw.Stop();
                log.Message = $"Request {request.GetType().Name} processed successfully";
                log.Status = ErrorStatus.Success;
                return result;
            }
            catch (Exception exception)
            {
                sw.Stop();
                log.Message = exception.Message;
                log.Status = ErrorStatus.Error;
                log.Exception = exception.StackTrace;

                if (exception is AppExceptionBase)
                {
                    log.Exception = ((AppExceptionBase)exception).GetDetails();
                    SetRequestId(request, exception);
                    throw;
                }

                throw new CMSApplicationException(request.RequestId, exception);
            }
            finally
            {
                log.ExecutionTime = sw.ElapsedMilliseconds;
                await _logRepository.InsertAsync(log);
                await _logRepository.SaveChangesAsync();
            }
        }

        private static void SetRequestId(TRequest request, Exception exception)
        {
            (exception as AppExceptionBase).RequestId = request.RequestId;
        }

        private Log SetLoggingInfo(TRequest request)
        {
            var userName = _httpContextAccessor.HttpContext != null &&
                        _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                        ? _httpContextAccessor.HttpContext.User.Identity.Name
                        : "Anonymous";

            var inputDetails = request.GetType().GetProperties()
                   .Where(p => p.Name != nameof(request.RequestId))
                   .Select(p => p.GetValue(request, null)).ToList();

            var inputDetailsAsJson = JsonConvert.SerializeObject(inputDetails);

            var log = new Log
            {
                RequestId = request.RequestId.ToString(),
                RequestName = request.RequestName,
                RequestType = request.RequestType,
                InputDetails = inputDetailsAsJson,
                UserName = userName
            };
            return log;
        }
    }
}
