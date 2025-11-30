using Microsoft.AspNetCore.Http.HttpResults;
using TaxManagement.Application.DTOs;
using TaxManagement.Domain.Entities;
using TaxManagement.Infrastructure.Database;

namespace TaxManagement.WebAPI.Endpoints
{
    public static class TaxRulesEndpoint
    {
        public static void MapTaxRulesEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/tax-rules")
                       .WithTags("Tax Rules");

            group.MapPost("/", Create)
                 .WithSummary("Creates a new Tax Rule");
        }

        // TODO: Clean architecture - Move logic to Application/Infra layer
        public static async Task<Results<Created, BadRequest>> Create(TaxRuleCreatedResponseDTO dto, AppDbContext context, CancellationToken cancellationToken)
        {
            var taxRule = new TaxRule(
                dto.OriginState,
                dto.DestinationState,
                dto.InterstateRate,
                dto.DifalRate,
                dto.FcpRate,
                dto.EffectiveDate
                );

            context.TaxRules.Add(taxRule);
            await context.SaveChangesAsync(cancellationToken);

            return TypedResults.Created();
        }
    }
}

