namespace MoneyManager.API.Contracts.UpdateRequests;

public record UpdateUserRequest(
    string Name,
    string Email,
    string BaseCurrency,
    string TimeZone);