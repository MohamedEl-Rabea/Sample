using System.Collections.Generic;

namespace Moj.CMS.Domain.Shared.Values
{
    /// <summary>
    ///A Value Object is an immutable type that is distinguishable only by the state of its properties.
    ///That is, unlike an Entity, which has a unique identifier and remains distinct even if its properties are otherwise identica
    ///</summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public bool ValueEquals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override bool Equals(object obj)
        {
            return ValueEquals(obj);    
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
