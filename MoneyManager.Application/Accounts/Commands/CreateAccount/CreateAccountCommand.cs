using MediatR;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(
    Guid UserId,
    string Title,
    AccountType Type,
    string Currency,
    decimal Balance
    ):IRequest<Guid>;