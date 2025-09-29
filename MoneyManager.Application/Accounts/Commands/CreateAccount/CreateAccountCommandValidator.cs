using FluentValidation;

namespace MoneyManager.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor (c => c.UserId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Type).IsInEnum();
        RuleFor(c => c.Currency).NotEmpty().Length(3, 4);
        RuleFor(c => c.Balance).GreaterThanOrEqualTo(0);
    }
}