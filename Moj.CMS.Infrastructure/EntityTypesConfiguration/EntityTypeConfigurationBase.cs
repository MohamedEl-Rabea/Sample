using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Shared.Values;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        public PropertyBuilder<LocalizedText> HasLocalizedText(EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, LocalizedText>> propertyExpression)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return builder.Property(propertyExpression).HasConversion(
            text => JsonConvert.SerializeObject(text, serializerSettings),
            text => JsonConvert.DeserializeObject<LocalizedText>(text, serializerSettings));
        }

        public PropertyBuilder<Type> HasSerializedType<Type>(EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, Type>> propertyExpression)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return builder.Property(propertyExpression).HasConversion(
            text => JsonConvert.SerializeObject(text, serializerSettings),
            text => (Type)JsonConvert.DeserializeObject(text, typeof(Type), serializerSettings));
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureEntity(builder);

            //Any other common logic could be added here
        }
    }
}
