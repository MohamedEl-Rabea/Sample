using Microsoft.Extensions.Logging;
using Moj.CMS.Integration.Contracts.Constants;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.Logging
{
    public class CustomLoggingScopeHttpMessageHandler : DelegatingHandler
    {
        private ILogger _logger;

        public CustomLoggingScopeHttpMessageHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var stopwatch = Stopwatch.StartNew();

            using (Log.BeginRequestPipelineScope(_logger, request))
            {
                Log.RequestPipelineStart(_logger, request);
                var response = await base.SendAsync(request, cancellationToken);
                Log.RequestPipelineEnd(_logger, response, stopwatch.ElapsedMilliseconds);

                return response;
            }
        }

        private static class Log
        {
            public static class EventIds
            {
                public static readonly EventId PipelineStart = new EventId(100, "RequestPipelineStart");
                public static readonly EventId PipelineEnd = new EventId(101, "RequestPipelineEnd");

                public static readonly EventId RequestHeader = new EventId(102, "RequestPipelineRequestHeader");
                public static readonly EventId ResponseHeader = new EventId(103, "RequestPipelineResponseHeader");
            }

            private static readonly Func<ILogger, HttpMethod, Uri, string, IDisposable> _beginRequestPipelineScope =
                LoggerMessage.DefineScope<HttpMethod, Uri, string>("HTTP {HttpMethod} {Uri} {CorrelationId}");

            private static readonly Action<ILogger, HttpMethod, Uri, string, Exception> _requestPipelineStart =
                LoggerMessage.Define<HttpMethod, Uri, string>(
                    LogLevel.Information,
                    EventIds.PipelineStart,
                    "Start processing HTTP request {HttpMethod} {Uri} [Correlation: {CorrelationId}]");

            private static readonly Action<ILogger, long, HttpStatusCode, Exception> _requestPipelineEnd =
                LoggerMessage.Define<long, HttpStatusCode>(
                    LogLevel.Information,
                    EventIds.PipelineEnd,
                    "End processing HTTP request after {ElapsedMilliseconds}ms - {StatusCode}");

            public static IDisposable BeginRequestPipelineScope(ILogger logger, HttpRequestMessage request)
            {
                var correlationId = GetCorrelationIdFromRequest(request);
                return _beginRequestPipelineScope(logger, request.Method, request.RequestUri, correlationId);
            }

            public static void RequestPipelineStart(ILogger logger, HttpRequestMessage request)
            {
                var correlationId = GetCorrelationIdFromRequest(request);
                _requestPipelineStart(logger, request.Method, request.RequestUri, correlationId, null);

                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.Log(
                        LogLevel.Trace,
                        EventIds.RequestHeader,
                        new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Request, request.Headers, request.Content?.Headers),
                        null,
                        (state, ex) => state.ToString());
                }
            }

            public static void RequestPipelineEnd(ILogger logger, HttpResponseMessage response, long durationInMilliseconds)
            {
                _requestPipelineEnd(logger, durationInMilliseconds, response.StatusCode, null);

                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.Log(
                        LogLevel.Trace,
                        EventIds.ResponseHeader,
                        new HttpHeadersLogValue(HttpHeadersLogValue.Kind.Response, response.Headers, response.Content?.Headers),
                        null,
                        (state, ex) => state.ToString());
                }
            }

            private static string GetCorrelationIdFromRequest(HttpRequestMessage request)
            {
                var correlationId = "Not set";

                if (request.Headers.TryGetValues(HttpHeadersConsts.CorrelationIdHeader, out var values))
                {
                    correlationId = values.First();
                }

                return correlationId;
            }
        }
    }
}