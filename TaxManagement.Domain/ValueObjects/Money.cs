namespace TaxManagement.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public Money(decimal amount)
    {
        Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }

    public static Money operator + (Money a, Money b) => new(a.Amount + b.Amount);
    public static Money operator - (Money a, Money b) => new(a.Amount - b.Amount);
    public static Money operator *(Money a, decimal multiplier) => new(a.Amount * multiplier);

    public override string ToString() => Amount.ToString("C", new System.Globalization.CultureInfo("pt-BR"));


    public static implicit operator decimal(Money money) => money.Amount;
    public static explicit operator Money(decimal amount) => new(amount);
}
