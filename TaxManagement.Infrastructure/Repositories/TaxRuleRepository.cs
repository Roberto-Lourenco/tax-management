using Microsoft.EntityFrameworkCore;
using TaxManagement.Domain.Entities;
using TaxManagement.Domain.Interfaces;
using TaxManagement.Infrastructure.Database;

namespace TaxManagement.Infrastructure.Repositories;

internal class TaxRuleRepository(AppDbContext context) : ITaxRuleRepository
{
    public async Task CreateAsync(TaxRule taxRule, CancellationToken ct)
    {
        context.TaxRules.Add(taxRule);
        await context.SaveChangesAsync(ct);
    }

    public async Task<TaxRule?> GetApplicableRuleAsync(string originState, string destinationState, DateTimeOffset targetDate, CancellationToken ct)
    {
        return await context.TaxRules
            .AsNoTracking()
            .Where(r => r.OriginState == originState &&
                        r.DestinationState == destinationState &&
                        r.EffectiveDate <= targetDate)
            .OrderByDescending(r => r.EffectiveDate)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await context.TaxRules
            .AnyAsync(r => r.Id == id, ct);
    }

    public async Task<TaxRule?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.TaxRules
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<IQueryable<TaxRule>> GetAllAsync(CancellationToken ct)
    {
        return context.TaxRules.AsNoTracking();
    }
}
