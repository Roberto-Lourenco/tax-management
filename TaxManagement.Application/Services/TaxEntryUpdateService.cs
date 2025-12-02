using TaxManagement.Domain.Common;
using TaxManagement.Domain.Entities;
using TaxManagement.Domain.Errors;
using TaxManagement.Domain.Interfaces;

namespace TaxManagement.Application.Services;

public class TaxEntryUpdateService(ITaxEntryRepository taxEntryRepository)
{
    public async Task<Result<TaxEntry>> UpdateStatusAsync(Guid id, TaxEntryStatusEnum newStatus, CancellationToken ct)
    {
        var taxEntry = await taxEntryRepository.GetByIdTrackingAsync(id, ct);

        if (taxEntry is null)
            return Result.Failure<TaxEntry>(Error.NotFound("TaxEntry.NotFound",
                $"Não foi encontrado nenhum Registro Fiscal correspondente ao ID informado."));

        var updateStatusResult = taxEntry.UpdateStatus(newStatus);

        if (updateStatusResult.IsFailure)
            return Result.Failure<TaxEntry>(updateStatusResult.Error);

        await taxEntryRepository.UpdateStatusAsync(taxEntry, ct);

        return Result.Success(taxEntry);
    }
}
