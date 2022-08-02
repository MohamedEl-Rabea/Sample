using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.Logging
{
    internal class CustomLoggingHttpMessageHandler : DelegatingHandler
    {
        private ILogger _logger;

        public CustomLoggingHttpMessageHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var stopwatch = Stopwatch.StartNew();

            // Not using a scope here because we always expect this to be at the end of the pipeline, thus there's
            // not really anything to surround.
            Log.RequestStart(_logger, request);
            var response = await base.SendAsync(request, cancellationToken);
            Log.RequestEnd(_logger, response, stopwatch.ElapsedMilliseconds);

            return response;
        }

        private static class Log
        {
            public static class EventIds
            {
                public static readonly EventId RequestStart = new EventId(100, "RequestStart");
                public static readonly EventId RequestEnd = new EventId(101, "RequestEnd");

                public static readonly EventId RequestHeader = new EventId(102, "RequestHeader");
                public static readonly EventId ResponseHeader = new EventId(103, "ResponseHeader");
            }

            private static readonly Action<ILogger, HttpMethod, Uri, Exception> _requestStart = LoggerMessage.Define<HttpMethod, Uri>(
                LogLevel.Information,
                EventIds.RequestStart,
                "Sending HTTP request {HttpMethod} {Uri}");

            private static readonly Action<ILogger, long, HttpStatusCode, Exception> _requestEnd = LoggerMessage.Define<long, HttpStatusCode>(
                LogLevel.Information,
                EventIds.RequestEnd,
                "Received HTTP response after {ElapsedMilliseconds}ms - {StatusCode}");

            public static void RequestStart(ILogger logger, HttpRequestMessage request)
            {
                _requestStart(logger, request.Method, request.RequestUri, null);

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

            public static void RequestEnd(ILogger logger, HttpResponseMessage response, long durationInMilliseconds)
            {
                _requestEnd(logger, durationInMilliseconds, response.StatusCode, null);

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
        }
    }
}