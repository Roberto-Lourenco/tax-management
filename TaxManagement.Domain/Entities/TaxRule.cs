namespace TaxManagement.Domain.Entities;

public class TaxRule
{
    public Guid Id { get; init; }
    public string OriginState { get; init; } = "RJ";
    public string DestinationState { get; init; }
    public decimal InterstateRate { get; init; }
    public decimal DifalRate { get; init; }
    public decimal FcpRate { get; init; }
    public DateTimeOffset EffectiveDate { get; private set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTime.UtcNow;
    public bool IsActive { get; private set; } = true;

    public TaxRule(string? originState,
        string destinationState,
        decimal interstateRate,
        decimal difalRate,
        decimal fcpRate,
        DateTimeOffset effectiveDate)
    {
        Id = Guid.NewGuid();
        OriginState = originState ?? OriginState;
        DestinationState = destinationState;
        InterstateRate = interstateRate;
        DifalRate = difalRate;
        FcpRate = fcpRate;
        EffectiveDate = effectiveDate;
    }

    // TODO: Adicionar métodos de validação
    public void Deactivate()
    {
        IsActive = false;
    }
}
