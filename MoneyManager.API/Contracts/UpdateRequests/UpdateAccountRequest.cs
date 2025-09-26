using MoneyManager.Domain.Enums;

namespace MoneyManager.API.Contracts.UpdateRequests;

public record UpdateAccountRequest(
    Guid UserId,
    string Title,
    AccountType Type,
    string Currency,
    bool IsArchived);