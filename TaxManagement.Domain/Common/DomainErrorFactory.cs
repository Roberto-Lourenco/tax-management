namespace TaxManagement.Domain.Common;

public class DomainRuleFactory
{
    private string EntityCode { get; init; }

    public DomainRuleFactory(string entityCode)
    {
        if (string.IsNullOrWhiteSpace(entityCode))
            throw new ArgumentException("O código de entidade não pode estar vazio ou ser nulo.", nameof(entityCode));

        EntityCode = entityCode;
    }

    public Error New(string fieldCode, string suffix, string message) =>
        Error.Validation(
            $"{EntityCode}.{fieldCode}.{suffix}",
            message);

    public Error Required(string fieldCode, string display) =>
        Error.Validation(
            $"{EntityCode}.{fieldCode}.Required",
            $"O campo {display} é obrigatório.");

    public Error CannotBeNegative(string fieldCode, string display) =>
        Error.Validation(
            $"{EntityCode}.{fieldCode}.CannotBeNegative",
            $"O campo {display} não pode ser negativo.");

    public Error AlreadyExists(string fieldCode,string display) =>
        Error.Conflict(
            $"{EntityCode}.{fieldCode}.AlreadyExists",
            $"Já existe um registro com o valor informado para o campo {display}.");

    public Error InvalidLength(string fieldCode,string display, int min, int max) =>
        Error.Validation(
            $"{EntityCode}.{fieldCode}.InvalidLength",
            $"O campo {display} deve ter entre {min} e {max} caracteres.");

    public Error InvalidFormat(string fieldCode, string display) =>
        Error.Validation(
            $"{EntityCode}.{fieldCode}.InvalidFormat",
            $"O campo {display} está em um formato inválido.");
}

