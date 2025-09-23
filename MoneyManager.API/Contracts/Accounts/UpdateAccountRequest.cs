using MoneyManager.Domain.Enums;

namespace MoneyManager.API.Contracts.Accounts;

public record UpdateAccountRequest(
    Guid UserId,
    string Title,
    AccountType Type,
    string Currency,
    bool IsArchived);