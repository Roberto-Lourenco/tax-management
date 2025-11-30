using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaxManagement.Application.DTOs;
using TaxManagement.Application.Services;
using TaxManagement.WebAPI.Extensions;

namespace TaxManagement.WebAPI.Endpoints.Routers.TaxEntries;

public static class TaxEntryRegistrationRoute
{
    public static async Task<Results<Ok<TaxEntryCreatedResponseDTO>, ProblemHttpResult>> HandleAsync(
        [FromBody] OrderRequestDTO orderReceivedDTO,
        [FromServices] TaxEntryRegistrationService service,
        CancellationToken ct)
    {

        var result = await service.HandleAsync(orderReceivedDTO, ct);

        if (result.IsFailure)
            return result.ToProblemDetails();

        var resultDto = new TaxEntryCreatedResponseDTO(
         result.Value.Id,
         result.Value.OrderId,
         result.Value.TotalOrderTax,
         result.Value.CreatedAt
         );

        return TypedResults.Ok(resultDto);
    }
}