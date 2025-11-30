using TaxManagement.Domain.Common;
using TaxManagement.Domain.Errors;

namespace TaxManagement.Domain.ValueObjects;

public record CompetenceDate
{
    private static readonly int CompetenceDateDefaultDay = 20;
    private static readonly int CompetenceDateDefaultHour = 11;
    public DateTimeOffset Value { get; private set; }
    private CompetenceDate(DateTimeOffset value)
    {
        Value = value;
    }
    public static Result<CompetenceDate> ConvertFromOrder(DateTimeOffset orderDate)
    {
        var competenceDate = ProcessCompetenceDate(orderDate);

        return new CompetenceDate(competenceDate);
    }

    public Result<CompetenceDate> PostPone(DateTimeOffset date)
    {
        if (Value > date)
        {
            return Result.Failure<CompetenceDate>(TaxEntryErrors.CompetenceDateCannotBeBeforeCurrent);
        }
        var competenceDate = ProcessCompetenceDate(date);
        return new CompetenceDate(competenceDate);
    }

    private static DateTimeOffset ProcessCompetenceDate(DateTimeOffset orderDate)
    {
        int monthsToAdd = 1;
        var targetDate = orderDate.AddMonths(monthsToAdd);

        return new DateTimeOffset(
            targetDate.Year,
            targetDate.Month,
            CompetenceDateDefaultDay,
            CompetenceDateDefaultHour, 0, 0, TimeSpan.Zero);
    }
    public static CompetenceDate Load(DateTimeOffset value) => new(value);
}
