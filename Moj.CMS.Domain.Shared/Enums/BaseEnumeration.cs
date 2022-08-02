using System;

namespace Moj.CMS.Domain.Shared.Enums
{
    public abstract class BaseEnumeration : IComparable
    {
        public string Name { get; }
        public int Id { get; }

        protected BaseEnumeration()
        {
        }

        protected BaseEnumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as BaseEnumeration;
            if (otherValue == null)
            {
                return false;
            }
            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);
            return typeMatches && valueMatches;
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((BaseEnumeration)other).Id);
        }

        public static bool operator ==(BaseEnumeration left, BaseEnumeration right)
        {
            if (ReferenceEquals(left, null))
            {
                if (ReferenceEquals(right, null))
                {
                    return true;
                }
                // Only the left side is null.
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(BaseEnumeration left, BaseEnumeration right)
        {
            return !(left == right);
        }
    }
}
