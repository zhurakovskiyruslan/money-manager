using FluentValidation;

namespace MoneyManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {

        RuleFor(t => t.Amount).GreaterThan(0).Must(a => decimal.Round(a, 2) == a)
            .WithMessage("Amount cannot have more than 2 decimal places.");
        RuleFor(x => x)
            .Must(c => c.SharedCategoryId.HasValue ^ c.CustomCategoryId.HasValue)
            .WithMessage("Exactly one type of category must be provided.");
        RuleFor(t => t.OccurredAt).NotEqual(default(DateTime))
            .LessThanOrEqualTo(_ => DateTime.UtcNow.AddMinutes(5));
        RuleFor(t => t.Description).MaximumLength(500);

    }
}