using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Shared.DTO
{
    public class MoneyDto
    {
        public MoneyDto()
        {

        }

        public MoneyDto(string currencyIso, decimal value)
        {
            CurrencyIso = currencyIso;
            Value = value;
        }

        public string CurrencyIso { get; set; }
        public decimal Value { get; set; }

        public Money ToValueObject()
        {
            return Money.Create(CurrencyIso, Value);
        }

        public static MoneyDto MapFromValueObject(Money money)
        {
            if (money == null)
                return new MoneyDto();

            return new MoneyDto { Value = money.Value, CurrencyIso = money.CurrencyIso };
        }

        public override string ToString()
        {
            return $"{Value} {CurrencyIso}";
        }

        public MoneyDto Subtract(MoneyDto input)
        {
            if (CurrencyIso != input.CurrencyIso)
            {
                throw new ArgumentException("Currencies are not matching");
            }

            return new MoneyDto { Value = Value - input.Value, CurrencyIso = CurrencyIso };
        }

        public static MoneyDto Default => new MoneyDto { Value = 0, CurrencyIso = "SAR" };
    }
}
