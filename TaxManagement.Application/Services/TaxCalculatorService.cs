using TaxManagement.Domain.Common;
using TaxManagement.Domain.Interfaces;

namespace TaxManagement.Application.Services;

public record TaxCalculatorResult(Guid RuleId, decimal TotalTax);

public class TaxCalculatorService(ITaxRuleRepository repo)
{
    public async Task<Result<TaxCalculatorResult>> HandleAsync(string originState, string customerState, decimal amount, DateTimeOffset orderDate, CancellationToken ct)
    {
        var rule = await repo.GetApplicableRuleAsync(originState, customerState, orderDate, ct);

        if (rule == null)
        {
            return Result.Failure<TaxCalculatorResult>(Error.UnprocessableEntity("TaxRules.NotFound", $"Não existe uma regra tributária entre '{originState}' e '{customerState}'")); //Todo: TaxRulesErrors
        }
        decimal totalRate = rule.InterstateRate + rule.DifalRate + rule.FcpRate;
        decimal totalTax = amount * totalRate;

        var result = new TaxCalculatorResult(rule.Id, totalTax);

        return Result.Success(result);
    }
}