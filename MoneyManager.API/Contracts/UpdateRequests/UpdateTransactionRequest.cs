namespace MoneyManager.API.Contracts.UpdateRequests;

public record UpdateTransactionRequest(Guid AccountId,
    Guid? SharedCategoryId,
    Guid? CustomCategoryId,
    decimal Amount,
    string? Description,
    DateTime OccurredAt);