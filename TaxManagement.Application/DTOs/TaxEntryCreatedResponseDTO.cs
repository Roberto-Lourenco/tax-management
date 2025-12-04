namespace TaxManagement.Application.DTOs;

public record TaxEntryCreatedResponseDTO(
    Guid Id,
    Guid OrderId,
    decimal TotalOrderTax,
    DateTimeOffset CreatedAt);
