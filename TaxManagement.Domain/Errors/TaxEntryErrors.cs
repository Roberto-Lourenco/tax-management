using TaxManagement.Domain.Common;

namespace TaxManagement.Domain.Errors;

public static class TaxEntryErrors
{
    public static readonly Error TaxEntryNotFound = Error.NotFound(
        "TaxEntry.Entity.NotFound",
        "Nenhum Registro Fiscal encontrado.");

    public static readonly Error OrderIdAlreadyExists = Error.Conflict(
        "TaxEntry.OrderId.AlreadyExists",
        "Já existe um Registro Fiscal com o mesmo ID de Pedido.");

    public static readonly Error TotalOrderAmountMustBePositive = Error.Validation(
        "TaxEntry.TotalOrderAmount.MustBePositive",
        "O valor total do pedido deve ser maior que zero.");

    public static readonly Error TotalOrderTaxMustBePositive = Error.Validation(
        "TaxEntry.TotalOrderTax.MustBePositive",
        "O valor total do imposto deve ser maior que zero");

    public static readonly Error OrderDateRequired = Error.Validation(
    "TaxEntry.OrderDate.Required",
    "A data do pedido é obrigatória.");

    public static readonly Error OrderDateCannotBeFuture = Error.Validation(
        "TaxEntry.OrderDate.CannotBeFuture",
        "A data do pedido não pode ser futura.");

    public static readonly Error CompetenceDateCannotBeBeforeCurrent = Error.Validation(
        "TaxEntry.CompetenceDate.CannotBeBeforeCurrent",
        "A nova data de competência não pode ser anterior à atual.");

    public static readonly Error CompetenceDateMaxOneMonthDelayExceeded = Error.Validation(
        "TaxEntry.CompetenceDate.MaxOneMonthDelayExceeded",
        "A nova data de competência excede o limite máximo permitido de atraso (1 mês).");

    public static readonly Error CompetenceDateInvalid = Error.Validation(
        "TaxEntry.CompetenceDate.InvalidDate",
        "A data de competência informada é inválida.");

    public static readonly Error StatusInvalidTransitionFromPaid = Error.Validation(
        "TaxEntry.Status.InvalidTransitionFromPaid",
        "Não é possível reverter um status Pago para Pendente.");

    public static readonly Error CompetenceDateFiscalYearChangeNotAllowed = Error.Validation(
        "TaxEntry.CompetenceDate.FiscalYearChangeNotAllowed",
        "A alteração do ano de competência não é permitida.");

    public static readonly Error StatusMustBeDifferent = Error.Validation(
        "TaxEntry.Status.MustBeDifferent",
        "O novo status deve ser diferente do atual.");

    public static readonly Error PaymentCannotBeSetManually = Error.Validation(
        "TaxEntry.Payment.CannotBeSetManually",
        "O pagamento não pode ser registrado manualmente.");

    public static readonly Error AuthenticationCodeInvalid = Error.Validation(
        "TaxEntry.AuthenticationCode.Invalid",
        "O código de autenticação do pagamento é inválido.");

    public static readonly Error OrderDateMustBeUtc = Error.Validation(
        "TaxEntry.OrderDate.MustBeUtc",
        "A data do pedido fornecida deve estar no formato UTC (GMT +0).");
}
