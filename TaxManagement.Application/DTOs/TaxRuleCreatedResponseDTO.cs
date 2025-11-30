namespace TaxManagement.Application.DTOs;

public record TaxRuleCreatedResponseDTO(
    string OriginState,
    string DestinationState,
    decimal InterstateRate,
    decimal DifalRate,
    decimal FcpRate,
    DateTime EffectiveDate);

