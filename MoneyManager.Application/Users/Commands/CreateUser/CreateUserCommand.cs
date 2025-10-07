using MediatR;

namespace MoneyManager.Application.Users.Commands.CreateUser;

public record CreateUserCommand( 
    string Name,
    string Email,
    string BaseCurrency,
    string TimeZone
): IRequest<Guid>;