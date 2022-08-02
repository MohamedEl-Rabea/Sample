using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Moj.CMS.Shared.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               configuration.ReadFrom.Configuration(context.Configuration);
           };
    }

}
