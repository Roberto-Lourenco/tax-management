namespace TaxManagement.Domain.Common;

public enum ErrorType
{
    None,
    Problem,
    Validation,
    NotFound,
    UnprocessableEntity,
    Conflict,
    Unauthorized
}

public record Error
{
    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    public static readonly Error NullValue = new(
        "Generic.NullError",
        "A operação falhou porque um valor ou recurso solicitado não existe ou é inválido.",
        ErrorType.Validation);

    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.Problem);

    public static Error Validation(string code, string description) =>
    new(code, description, ErrorType.Validation);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error UnprocessableEntity(string code, string description) =>
    new(code, description, ErrorType.UnprocessableEntity);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Unauthorized(string code, string description) =>
        new(code, description, ErrorType.Unauthorized);

}