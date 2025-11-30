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

public class TaxEntry
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public DateTimeOffset OrderDate { get; init; }
    public TaxEntryStatusEnum Status { get; set; }
    public CompetenceDate CompetenceDate { get; private set; }
    public Guid TaxRuleId { get; private set; }
    public decimal TotalOrderAmount { get; init; }
    public decimal TotalOrderTax { get; private set; }
    public string? PaymentAuthenticationCode { get; private set; }
    public DateTimeOffset? PaymentDate { get; private set; }
    public DateTime CreatedAt { get; init; }

    private TaxEntry()
    {
        CompetenceDate = null!;
    }

    public static Result<TaxEntry> Create(
        Guid orderId,
        DateTimeOffset orderDate,
        Guid taxRuleId,
        decimal totalOrderAmount,
        decimal totalOrderTax)
    {
        
        orderDate = orderDate.ToUniversalTime();

        var validation = ValidationError.Compose(
            Result.Ensure(totalOrderAmount > 0,
            TaxEntryErrors.TotalOrderAmountMustBePositive),
            Result.Ensure(totalOrderTax > 0,
            TaxEntryErrors.TotalOrderTaxMustBePositive),
            Result.Ensure(orderDate != default, TaxEntryErrors.OrderDateRequired),
            Result.Ensure(orderDate <= DateTimeOffset.UtcNow.AddMinutes(5), TaxEntryErrors.OrderDateCannotBeFuture)
        );

        if (validation.IsFailure)
            return Result.Failure<TaxEntry>(validation.Error);

        var newCompetenceDate = CompetenceDate.ConvertFromOrder(orderDate).Value;

        return new TaxEntry
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            OrderDate = orderDate,
            Status = TaxEntryStatusEnum.Pending,
            CompetenceDate = newCompetenceDate,
            TaxRuleId = taxRuleId,
            TotalOrderAmount = totalOrderAmount,
            TotalOrderTax = totalOrderTax,
            CreatedAt = DateTime.UtcNow
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
            TaxEntryErrors.AuthenticationCodeInvalid)
        );

        if (validation.IsFailure)
            return Result.Failure<TaxEntry>(validation.Error);

        PaymentAuthenticationCode = paymentAuthenticationCode;
        PaymentDate = paymentDate;
        Status = TaxEntryStatusEnum.Paid;
        return Result.Success(this);
    }

    // TODO
    public Result<TaxEntry> PostPoneCompetenceDate(DateTime newCompetenceDate)
    {
        var result = CompetenceDate.PostPone(newCompetenceDate);

        if (result.IsFailure)
            return Result.Failure<TaxEntry>(result.Error);

        CompetenceDate = result.Value;

        return Result.Success(this);
    }
}

