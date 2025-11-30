using TaxManagement.Application.DTOs;
using TaxManagement.Domain.Common;
using TaxManagement.Domain.Entities;
using TaxManagement.Domain.Errors;
using TaxManagement.Domain.Interfaces;

namespace TaxManagement.Application.Services;

public class TaxEntryRegistrationService(ITaxEntryRepository taxEntryRepository, TaxCalculatorService taxCalculator)
{
    public async Task<Result<TaxEntry>> HandleAsync(OrderRequestDTO orderDto, CancellationToken ct)
    {
        bool orderExists = await taxEntryRepository.ExistsByOrderIdAsync(orderDto.OrderId, ct);

        if (orderExists)
            return Result.Failure<TaxEntry>(TaxEntryErrors.OrderIdAlreadyExists);

        var calculationResult = await taxCalculator.HandleAsync(
            orderDto.OriginState,
            orderDto.CustomerState,
            orderDto.TotalAmountReceived,
            orderDto.OrderDate,
            ct);

        if (calculationResult.IsFailure)
            return Result.Failure<TaxEntry>(calculationResult.Error);

        var taxResult = calculationResult.Value;

        var taxEntry = TaxEntry.Create(
            orderDto.OrderId,
            orderDto.OrderDate,
            taxResult.RuleId,
            orderDto.TotalAmountReceived,
            taxResult.TotalTax);

        if (taxEntry.IsFailure)
            return Result.Failure<TaxEntry>(taxEntry.Error);

        await taxEntryRepository.CreateAsync(taxEntry.Value, ct);

        return Result.Success(taxEntry.Value);

    }
}
