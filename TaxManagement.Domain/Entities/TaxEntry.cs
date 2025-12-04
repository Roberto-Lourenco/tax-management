using TaxManagement.Domain.Common;
using TaxManagement.Domain.Errors;
using TaxManagement.Domain.ValueObjects;

namespace TaxManagement.Domain.Entities;

public enum TaxEntryStatusEnum
{
    Pending,
    Paid,
    Overdue,
    Cancelled,
    Failed
}

public sealed class TaxEntry
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public DateTimeOffset OrderDate { get; init; }
    public TaxEntryStatusEnum Status { get; private set; }
    public CompetenceDate CompetenceDate { get; private set; }
    public Guid TaxRuleId { get; private set; }
    public Money TotalOrderAmount { get; init; }
    public Money TotalOrderTax { get; private set; }
    public string? PaymentAuthenticationCode { get; private set; }
    public DateTimeOffset? PaymentDate { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }

    private TaxEntry()
    {
        CompetenceDate = null!;
        TotalOrderAmount = null!;
        TotalOrderTax = null!;
    }

    public static Result<TaxEntry> Create(
        Guid orderId,
        DateTimeOffset orderDate,
        Guid taxRuleId,
        decimal totalOrderAmount,
        decimal totalOrderTax)
    {
        var now = DateTimeOffset.UtcNow;
        var sanitizedOrderDate = orderDate.ToUniversalTime();
        int minutesTolerance = 5;

        var validation = ValidationError.Compose(
            Result.Ensure(totalOrderAmount >= 0,
            TaxEntryErrors.TotalOrderAmountCannotBeNegative),

            Result.Ensure(totalOrderTax >= 0,
            TaxEntryErrors.TotalOrderTaxCannotBeNegative),

            Result.Ensure(sanitizedOrderDate != default, TaxEntryErrors.OrderDateRequired),
            Result.Ensure(sanitizedOrderDate <= now.AddMinutes(minutesTolerance), TaxEntryErrors.OrderDateCannotBeFuture)
        );

        if (validation.IsFailure)
            return Result.Failure<TaxEntry>(validation.Error);

        var resultCompetenceDate = CompetenceDate.ConvertFromOrder(orderDate);

        return new TaxEntry
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            OrderDate = orderDate,
            Status = TaxEntryStatusEnum.Pending,
            CompetenceDate = resultCompetenceDate.Value,
            TaxRuleId = taxRuleId,
            TotalOrderAmount = new Money(totalOrderAmount),
            TotalOrderTax = new Money(totalOrderTax),
            CreatedAt = now
        };
    }

    public Result<TaxEntry> UpdateStatus(TaxEntryStatusEnum newStatus)
    {
        var validation = ValidationError.Compose(
            Result.Ensure(newStatus != Status,
            TaxEntryErrors.StatusMustBeDifferent),

            Result.Ensure(Status != TaxEntryStatusEnum.Paid,
            TaxEntryErrors.StatusInvalidTransitionFromPaid)
        );

        if(validation.IsFailure)
            return Result.Failure<TaxEntry>(validation.Error);

        Status = newStatus;

        return Result.Success(this);
    }

    // TODO
    public Result<TaxEntry> RegisterPayment(string paymentAuthenticationCode, DateTime paymentDate)
    {
        var validation = ValidationError.Compose(
            Result.Ensure(!string.IsNullOrWhiteSpace(paymentAuthenticationCode),
            TaxEntryErrors.PaymentAuthenticationCodeInvalid)
        );

        if (validation.IsFailure)
            return Result.Failure<TaxEntry>(validation.Error);

        PaymentAuthenticationCode = paymentAuthenticationCode;
        PaymentDate = paymentDate;
        Status = TaxEntryStatusEnum.Paid;
        return Result.Success(this);
    }

    public Result<TaxEntry> PostPoneCompetenceDate(DateTime newCompetenceDate)
    {
        var result = CompetenceDate.PostPone(newCompetenceDate);

        if (result.IsFailure)
            return Result.Failure<TaxEntry>(result.Error);

        CompetenceDate = result.Value;

        return Result.Success(this);
    }
}

