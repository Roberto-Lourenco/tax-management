using TaxManagement.Domain.Entities;

namespace TaxManagement.Domain.Interfaces;

public interface ITaxRuleRepository
{
    Task CreateAsync(TaxRule rule, CancellationToken ct);
    Task<TaxRule?> GetApplicableRuleAsync(string originState, string destinationState, DateTimeOffset targetDate, CancellationToken ct);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    Task<TaxRule?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IQueryable<TaxRule>> GetAllAsync(CancellationToken ct);

}
