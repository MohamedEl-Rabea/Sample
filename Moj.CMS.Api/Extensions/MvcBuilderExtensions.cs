using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Application.AppServices.Case.Commands.AddCase;
using Moj.CMS.UserAccess.Application.Validators;

namespace Moj.CMS.Api.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            // to register validations from different assemblies (modules)
            builder.AddFluentValidation(fv =>
            {
                fv.LocalizationEnabled = false;
                fv.ImplicitlyValidateRootCollectionElements = true;
                fv.ImplicitlyValidateChildProperties = true;
                fv.RegisterValidatorsFromAssemblyContaining<TokenRequestValidator>(); // user access module
                fv.RegisterValidatorsFromAssemblyContaining<AddNewCaseListCommandValidator>(); // cms access 
            });
            return builder;
        }
    }
}