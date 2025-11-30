using TaxManagement.Domain.Entities;

namespace TaxManagement.Domain.Interfaces;

public interface ITaxEntryRepository
{
    Task<bool> ExistsAsync (Guid orderId, CancellationToken ct);
    Task<bool> ExistsByOrderIdAsync(Guid orderId, CancellationToken ct);
    Task CreateAsync (TaxEntry taxEntry, CancellationToken ct);
    Task<TaxEntry?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<TaxEntry?> GetByIdTrackingAsync(Guid id, CancellationToken ct);
    Task UpdateStatusAsync (TaxEntry taxEntry, CancellationToken ct);

    // TODO: Implement filtering method when needed
    //Task<IQueryable<TaxEntry>> GetByFilterAsync (TaxEntryFilter filter, CancellationToken ct);

}
