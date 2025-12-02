using TaxManagement.Domain.Common;
using TaxManagement.Domain.Entities;

namespace TaxManagement.Domain.Errors;

// Display name simples para campos de TaxEntry, utilizado apenas em mensagens de erro.
// Como não haverá internacionalização, os nomes estarão em português
internal static class TaxEntryDisplay
{
    internal const string OrderId = "Ordem de pedido";
    internal const string OrderDate = "Data do pedido";
    internal const string Status = "Status";
    internal const string CompetenceDate = "Data de competência";
    internal const string TaxRuleId = "ID da Regra fiscal";
    internal const string TotalOrderAmount = "Valor total do pedido";
    internal const string TotalOrderTax = "Imposto total do pedido";
    internal const string PaymentAuthenticationCode = "Código de autenticação de pagamento";
}

public static class TaxEntryErrors
{
    private readonly static DomainRuleFactory Rule = new(nameof(TaxEntry));

    // OrderId
    public static readonly Error OrderIdRequired = Rule.Required(
        nameof(TaxEntry.OrderId),
        TaxEntryDisplay.OrderId);

    public static readonly Error OrderIdAlreadyExists = Rule.AlreadyExists(
        nameof(TaxEntry.OrderId),
        TaxEntryDisplay.OrderId);

    // TotalOrderAmount
    public static readonly Error TotalOrderAmountRequired = Rule.Required(
        nameof(TaxEntry.TotalOrderAmount),
        TaxEntryDisplay.TotalOrderAmount);

    public static readonly Error TotalOrderAmountCannotBeNegative = Rule.CannotBeNegative(
        nameof(TaxEntry.TotalOrderAmount),
        TaxEntryDisplay.TotalOrderAmount);

    // TotalOrderTax
    public static readonly Error TotalOrderTaxRequired = Rule.Required(
        nameof(TaxEntry.TotalOrderTax),
        TaxEntryDisplay.TotalOrderTax);

    public static readonly Error TotalOrderTaxCannotBeNegative = Rule.CannotBeNegative(
        nameof(TaxEntry.TotalOrderTax),
        TaxEntryDisplay.TotalOrderTax);

    // OrderDate
    public static readonly Error OrderDateRequired = Rule.Required(
        nameof(TaxEntry.OrderDate),
        TaxEntryDisplay.OrderDate);

    public static readonly Error OrderDateInvalid = Rule.InvalidFormat(
        nameof(TaxEntry.OrderDate),
        TaxEntryDisplay.OrderDate);

    public static readonly Error OrderDateCannotBeFuture = Rule.New(
        nameof(TaxEntry.OrderDate),
        "CannotBeFuture",
        $"A {TaxEntryDisplay.OrderDate} não pode estar em uma data futura."
        );

    // CompetenceDate
    public static readonly Error CompetenceDateRequired = Rule.Required(
        nameof(TaxEntry.CompetenceDate),
        TaxEntryDisplay.CompetenceDate);

    public static readonly Error CompetenceDateCannotBeBeforeCurrent = Rule.New(
        nameof(TaxEntry.CompetenceDate),
        "CannotBeBeforeCurrent",
        $"A nova {TaxEntryDisplay.CompetenceDate} não pode ser anterior à atual.");

    public static readonly Error CompetenceDateMaxOneMonthDelayExceeded = Rule.New(
        nameof(TaxEntry.CompetenceDate),
        "MaxOneMonthDelayExceeded",
        $"A nova {TaxEntryDisplay.CompetenceDate} excede o limite máximo permitido de atraso (1 mês).");

    public static readonly Error CompetenceDateInvalid = Rule.InvalidFormat(
        nameof(TaxEntry.CompetenceDate),
        TaxEntryDisplay.CompetenceDate);

    public static readonly Error CompetenceDateFiscalYearChangeNotAllowed = Rule.New(
        nameof(TaxEntry.CompetenceDate),
        "FiscalYearChangeNotAllowed",
        $"A alteração do ano da {TaxEntryDisplay.CompetenceDate} não é permitida.");

    // Status
    public static readonly Error StatusInvalidTransitionFromPaid = Rule.New(
        nameof(TaxEntry.Status),
        "InvalidTransitionFromPaid",
        $"Não é possível reverter um {TaxEntryDisplay.Status} Pago para Pendente.");

    public static readonly Error StatusMustBeDifferent = Rule.New(
        nameof(TaxEntry.Status),
        "MustBeDifferent",
        $"O novo {TaxEntryDisplay.Status} deve ser diferente do atual.");

    // PaymentAuthenticationCode
    public static readonly Error PaymentAuthenticationCodeInvalid = Rule.InvalidFormat(
        nameof(TaxEntry.PaymentAuthenticationCode),
        TaxEntryDisplay.PaymentAuthenticationCode);
}
