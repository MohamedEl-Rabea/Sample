using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Text.Json.Serialization;
namespace Moj.CMS.Api.Filters
{
    public class SwaggerSkipPropertyFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }
            if (typeof(IHasIgnoredProperties).IsAssignableFrom(context.Type))
            {
                var instance = (IHasIgnoredProperties)Activator.CreateInstance(context.Type);
                var skipProperties = instance.GetIgnoredProperties();
                foreach (var skipProperty in skipProperties)
                {
                    var propertyToSkip = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, skipProperty, StringComparison.OrdinalIgnoreCase));
                    if (propertyToSkip != null)
                    {
                        schema.Properties.Remove(propertyToSkip);
                    }
                }
            }
        }
    }
}
