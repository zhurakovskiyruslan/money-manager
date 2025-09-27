using FluentValidation;

namespace MoneyManager.Application.CustomCategories.Commands.CreateCustomCategory;

public class CreateCustomCategoryCommandValidator : AbstractValidator<CreateCustomCategoryCommand>
{
    public CreateCustomCategoryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Type).IsInEnum();
    }
}