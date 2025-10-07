namespace MoneyManager.API.Contracts.UpdateRequests;

public record UpdateTransferRequest(
    Guid SourceAccountId,
    Guid DestinationAccountId,
    decimal SourceAmount,
    decimal DestinationAmount,
    string? Description,
    DateTime OccurredAt
    );