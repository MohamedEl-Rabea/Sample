using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Api.Extensions;
using Moj.CMS.Api.Validation;
using Moj.CMS.Domain.Shared.Exceptions;
using Moj.CMS.Settings.Shared.Extensions;
using Moj.CMS.Settings.Shared.Helpers;
using Moj.CMS.Shared.Exceptions;
using System;

namespace Moj.CMS.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonServices(_configuration, _env);
            services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));
            services.RegisterSwagger();
            services.AddControllers().AddValidators();

            //TODO: should be applied per controller
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, exception) => false;
                options.Map<DuplicateEntryException>(ex => new DuplicateEntryProplemDetails(ex));
                options.Map<CMSApplicationException>(ex => new ApplicationProblemDetails(ex));
                options.Map<InvalidRequestException>(ex => new InvalidRequstProblemDetails(ex));
                options.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            app.ConfigureSwagger();

            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRequestLocalizationByCulture();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard(_env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Initialize(app.ApplicationServices, _env);
        }

        private void Initialize(IServiceProvider applicationServices, IWebHostEnvironment env)
        {
            using var scope = applicationServices.CreateScope();
            var appInitializer = scope.ServiceProvider.GetService<IAppInitializer>();
            appInitializer.Initialize(scope.ServiceProvider, env.EnvironmentName);
        }
    }
}
