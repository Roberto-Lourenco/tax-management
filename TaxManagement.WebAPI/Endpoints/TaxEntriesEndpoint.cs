using TaxManagement.WebAPI.Endpoints.Routers.TaxEntries;

namespace TaxManagement.WebAPI.Endpoints;

public static class TaxEntriesEndpoint
{
    public static void MapTaxEntriesEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tax-entries")
                       .WithTags("Tax Entries");

        group.MapPost("/", TaxEntryRegistrationRoute.HandleAsync)
             .WithSummary("Creates a new tax entry based on an order");
    }
}
