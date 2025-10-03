using FluentValidation;

namespace MoneyManager.Application.Transactions.Queries;

public class GetTransactionsValidator : AbstractValidator<GetTransactionsQuery>
{
    public GetTransactionsValidator()
    {
        RuleFor(x=>x.From)
            .NotEmpty()
            .Must(IsUtc)
            .WithMessage("from must be UTC (e.g. 2025-01-31T15:45:00Z).");
        RuleFor(x=>x.To).NotEmpty().
            Must(IsUtc)
            .WithMessage("from must be UTC (e.g. 2025-01-31T15:45:00Z).");
        RuleFor(x => x)
            .Must(x => x.From <= x.To)
            .WithMessage("'from' must be <= 'to'.");
    }
    
    private static bool IsUtc(DateTimeOffset dt) =>
      dt.Offset == TimeSpan.Zero;

}