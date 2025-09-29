using MediatR;

namespace MoneyManager.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) :IRequest<Unit>;