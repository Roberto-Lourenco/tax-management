namespace TaxManagement.Domain.Common;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "Validation.Failed",
            "Ocorreram um ou mais erros de validação.",
            ErrorType.Validation)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static Result Compose(params Result[] results)
    {
        var failures = results
            .Where(r => r.IsFailure)
            .Select(r => r.Error)
            .ToArray();

        if (failures.Length == 0) return Result.Success();

        if (failures.Length == 1) return Result.Failure(failures[0]);

        return Result.Failure(new ValidationError(failures));
    }
}
