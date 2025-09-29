using FluentValidation.Results;

namespace MoneyManager.Application.Common.Exceptions;

public class ApplicationValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public ApplicationValidationException(IEnumerable<ValidationFailure> failures,Exception? inner = null)
        : base("Validation failed", inner)
    {
        Errors = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ErrorMessage).Distinct().ToArray()
            );
    }

    public static ApplicationValidationException Single(string message, string key = "General")
    {
        return new ApplicationValidationException(new[]
        {
            new ValidationFailure(key, message)
        });
    }
}