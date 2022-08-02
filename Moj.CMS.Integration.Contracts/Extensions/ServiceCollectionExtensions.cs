using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Moj.CMS.Integration.ClientGenerator.Services.TahseelService;
using Moj.CMS.Integration.Contracts.AlAhli_B2B;
using Moj.CMS.Integration.Contracts.Handlers;
using Moj.CMS.Integration.Contracts.Logging;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Moj.CMS.Integration.Contracts.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTahseelServiceAgents(this IServiceCollection services, TahseelApiOptions options)
        {
            services.AddTransient<HttpContextHandler>();

            services.AddHttpClient<ITahseelClient, TahseelClient>(client =>
            {
                client.BaseAddress = new Uri(options.BaseAddress);
                var tahseelService = new TahseelService(options.BaseAddress, client);
                return new TahseelClient(tahseelService, options);
            }).AddHttpMessageHandler<HttpContextHandler>()
            .AddPollyHandlers();

            services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, CustomLoggingFilter>());
        }

        public static void AddAlAhliB2BServiceAgents(this IServiceCollection services, AlahliApiOptions options)
        {
            services.AddTransient<HttpContextHandler>();

            services.AddHttpClient<IAlahliClient, AlahliClient>(client =>
            {
                client.BaseAddress = new Uri(options.BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken);
                return new AlahliClient(client, options);
            }).AddHttpMessageHandler<HttpContextHandler>()
            .AddPollyHandlers();

            services.Replace(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, CustomLoggingFilter>());
        }

        private static void SetDefaultHeaders(TahseelApiOptions options, HttpClient client)
        {
            foreach (var header in options.DefaultRequestHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public static IHttpClientBuilder AddPollyHandlers(this IHttpClientBuilder httpClientBuilder)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circutBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(60));

            //To be used in some condtiotional scenarios later
            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            return httpClientBuilder
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(circutBreakerPolicy);
        }
    }
}
