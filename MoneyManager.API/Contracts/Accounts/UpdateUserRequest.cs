namespace MoneyManager.API.Contracts.Accounts;

public record UpdateUserRequest(
    string Name,
    string Email,
    string BaseCurrency,
    string TimeZone);