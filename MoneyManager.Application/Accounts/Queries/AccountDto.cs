using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Accounts.Queries;

public record AccountDto(
    string Title,
    AccountType Type,
    string Currency,
    decimal Balance);