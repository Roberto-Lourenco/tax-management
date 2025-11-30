using Microsoft.EntityFrameworkCore;
using TaxManagement.Domain.Entities;
using TaxManagement.Domain.Interfaces;
using TaxManagement.Infrastructure.Database;

namespace TaxManagement.Infrastructure.Repositories;

internal class TaxEntryRepository(AppDbContext context) : ITaxEntryRepository
{
    public async Task CreateAsync(TaxEntry taxEntry, CancellationToken ct)
    {
        context.TaxEntries.Add(taxEntry);

        await context.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await context.TaxEntries
            .AnyAsync(t => t.Id == id, ct);
    }

    public async Task<bool> ExistsByOrderIdAsync(Guid orderId, CancellationToken ct)
    {
        return await context.TaxEntries
            .AnyAsync(t => t.OrderId == orderId, ct);
    }

    public async Task<TaxEntry?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.TaxEntries
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
    public async Task<TaxEntry?> GetByIdTrackingAsync(Guid id, CancellationToken ct)
    {
        return await context.TaxEntries
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task UpdateStatusAsync(TaxEntry taxEntry, CancellationToken ct)
    {
        var entry = context.Entry(taxEntry);

        if (entry.State == EntityState.Detached)
            context.TaxEntries.Update(taxEntry);

        await context.SaveChangesAsync(ct);
    }
}
