using FluentValidation;

namespace MoneyManager.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Name).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().MaximumLength(254).EmailAddress();
        RuleFor(u => u.BaseCurrency).NotEmpty().Length(3);
        RuleFor(u => u.TimeZone).NotEmpty().Must(BeAValidTimeZone);
    }
    private bool BeAValidTimeZone(string tz) =>
        TimeZoneInfo.GetSystemTimeZones().Any(z => z.Id == tz);
}