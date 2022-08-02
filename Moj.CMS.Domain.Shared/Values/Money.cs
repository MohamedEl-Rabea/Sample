using System;
using System.Collections.Generic;
namespace Moj.CMS.Domain.Shared.Values
{

    public class Money : ValueObject
    {
        public string CurrencyIso { get; private set; }
        public decimal Value { get; private set; }
        private Money()
        {

        }

        public static Money Create(string CurrencyIso, decimal Value)
        {
            Guard.Guard.AssertArgumentNotNullOrEmptyOrWhitespace(CurrencyIso, nameof(CurrencyIso));
            Guard.Guard.AssertArgumentNotNull(Value, nameof(Value));
            Guard.Guard.AssertArgumentNotLessThan(Value, 0, nameof(Value));

            var Money = new Money
            {
                CurrencyIso = CurrencyIso.ToUpper(),
                Value = Value
            };
            return Money;
        }

        public Money Add(Money amount)
        {
            if (amount.ValueEquals(Money.Empty))
                return Clone();

            if (ValueEquals(Empty))
                return amount.Clone();

            if (amount.CurrencyIso != CurrencyIso)
                throw new ArgumentException("Cannot subtract two money values with different currencies");

            return new Money { Value = Value + amount.Value, CurrencyIso = amount.CurrencyIso };
        }

        public Money Subtract(Money amount)
        {
            if (amount.ValueEquals(Money.Empty))
                return Clone();

            if (ValueEquals(Empty))
                return amount.Clone();

            if (amount.CurrencyIso != CurrencyIso)
                throw new ArgumentException("Cannot subtract two money values with different currencies");

            return new Money { Value = Value - amount.Value, CurrencyIso = amount.CurrencyIso };
        }

        public static Money Empty => new Money { CurrencyIso = string.Empty, Value = 0 };

        public override string ToString()
        {
            return $"{Value} - {CurrencyIso}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencyIso;
            yield return Value;
        }

        public Money Clone()
        {
            return new Money
            {
                CurrencyIso = CurrencyIso,
                Value = Value
            };
        }

        public static bool operator ==(Money left, Money right)
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

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        public static bool operator >(Money left, Money right)
        {
            if (!IsCurrencyEqual(left.CurrencyIso, right.CurrencyIso))
            {
                throw new Exception("Currency ISO not equal");
            }

            if (left.Value > right.Value)
                return true;

            return false;
        }

        public static bool operator <(Money left, Money right)
        {
            if (!IsCurrencyEqual(left.CurrencyIso, right.CurrencyIso))
            {
                throw new Exception("Currency ISO not equal");
            }

            if (left.Value < right.Value)
                return true;

            return false;
        }

        public static bool operator <=(Money left, Money right)
        {
            if (!IsCurrencyEqual(left.CurrencyIso, right.CurrencyIso))
            {
                throw new Exception("Currency ISO not equal");
            }

            if (left.Value <= right.Value)
                return true;

            return false;
        }

        public static bool operator >=(Money left, Money right)
        {
            if (!IsCurrencyEqual(left.CurrencyIso, right.CurrencyIso))
            {
                throw new Exception("Currency ISO not equal");
            }

            if (left.Value >= right.Value)
                return true;

            return false;
        }

        public static bool IsCurrencyEqual(string left, string right)
        {
            return left.Equals(right) ? true : false;
        }
    }
}
