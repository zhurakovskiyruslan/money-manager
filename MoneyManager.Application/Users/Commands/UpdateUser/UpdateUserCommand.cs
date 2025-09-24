using MediatR;

namespace MoneyManager.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string Name,
    string Email,
    string BaseCurrency,
    string TimeZone)
    : IRequest<Guid>;