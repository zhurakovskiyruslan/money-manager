using FluentValidation;

namespace MoneyManager.Application.CustomCategories.Commands.UpdateCustomCategory;

public class UpdateCustomCategoryValidator :  AbstractValidator<UpdateCustomCategoryCommand>
{
    public UpdateCustomCategoryValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
    }
}