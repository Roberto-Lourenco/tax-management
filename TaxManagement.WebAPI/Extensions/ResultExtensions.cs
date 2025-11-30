using Microsoft.AspNetCore.Http.HttpResults;
using TaxManagement.Domain.Common;

namespace TaxManagement.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ProblemHttpResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Não é possível converter Result.Success em ProblemDetails.");
        }

        return TypedResults.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            detail: GetDetail(result.Error),
            extensions: new Dictionary<string, object?>
            {
                { 
                    "errors",
                    GetErrors(result.Error)
                        .Select(e => new { e.Code, e.Description })
                }
            });
    }

    /// <summary>
    /// Guard Clause para erro humano.
    /// Retorna a descrição do erro. Se for ErrorType.Problem, retorna uma mensagem genérica.
    /// </summary>
    private static string GetDetail(Error error)
    {
        if (error.Type != ErrorType.Problem)
        {
            return error.Description;
        }
        return "Ocorreu um erro interno ao processar sua solicitação.";
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.UnprocessableEntity => "Unprocessable Entity",
            ErrorType.Unauthorized => "Unauthorized",
            _ => "Server Error"
        };

    private static IEnumerable<Error> GetErrors(Error error)
    {
        if (error is ValidationError validationError)
        {
            return validationError.Errors;
        }

        return new[] { error };
    }
}