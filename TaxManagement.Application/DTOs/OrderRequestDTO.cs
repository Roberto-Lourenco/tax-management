namespace TaxManagement.Application.DTOs;

public record OrderRequestDTO(
    Guid OrderId,
    decimal TotalAmountReceived,
    string OriginState,
    string CustomerState,
    DateTime OrderDate
    //Todo: List<object>Produtos
    );