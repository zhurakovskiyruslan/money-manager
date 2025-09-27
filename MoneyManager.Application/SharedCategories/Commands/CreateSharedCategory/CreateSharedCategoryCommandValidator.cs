using FluentValidation;

namespace MoneyManager.Application.SharedCategories.Commands.CreateSharedCategory;

public class CreateSharedCategoryCommandValidator: AbstractValidator<CreateSharedCategoryCommand>
{
    public CreateSharedCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Type)
            .NotEmpty().IsInEnum();
    }
}