using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Exceptions;

namespace Moj.CMS.Domain.Shared.Entities
{

    public abstract class Entity : Entity<int>, IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IEntity"/> for aggregate roots.
    /// 
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            //EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        public virtual bool EntityEquals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (Entity<TPrimaryKey>)obj;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        protected async Task CheckRuleAsync(IAsyncBusinessRule rule)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public override bool Equals(object obj)
        {
            return EntityEquals(obj);    
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
