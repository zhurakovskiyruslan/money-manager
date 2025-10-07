using FluentValidation;

namespace MoneyManager.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(x => x.SourceAccountId)
            .NotEmpty()
            .NotEqual(x => x.DestinationAccountId)
            .WithMessage("Source and destination accounts must differ");
        RuleFor(x => x.DestinationAccountId).NotEmpty();    
        RuleFor(x => x.SourceAmount)
            .NotEmpty().GreaterThan(0)
            .Must(a => decimal.Round(a, 2) == a)
            .WithMessage("Amount cannot have more than 2 decimal places.");
        RuleFor(x => x.DestinationAmount)
            .NotEmpty().GreaterThan(0)
            .Must(a => decimal.Round(a, 2) == a)
            .WithMessage("Amount cannot have more than 2 decimal places.");
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.OccurredAt)
            .NotEqual(default(DateTime))
            .LessThanOrEqualTo(_ => DateTime.UtcNow.AddMinutes(5));
    }
}