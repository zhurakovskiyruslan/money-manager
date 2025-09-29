using MediatR;

namespace MoneyManager.Application.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand(Guid Id) :  IRequest<Unit>;