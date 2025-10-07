using MediatR;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Accounts.Commands.UpdateAccount;

public record UpdateAccountCommand(
    Guid Id,
    Guid UserId,
    string Title,
    AccountType Type,
    string Currency,
    bool IsArchived
    ) : IRequest<Guid>;