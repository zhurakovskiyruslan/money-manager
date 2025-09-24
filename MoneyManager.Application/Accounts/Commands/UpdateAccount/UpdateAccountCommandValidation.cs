using FluentValidation;
namespace MoneyManager.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor (c => c.UserId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Type).IsInEnum();
        RuleFor(c => c.Currency).NotEmpty().Length(3, 4);
        
    }
}