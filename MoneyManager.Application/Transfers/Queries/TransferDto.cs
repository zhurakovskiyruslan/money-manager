namespace MoneyManager.Application.Transfers.Queries;

public record TransferDto(
    Guid SourceAccountId,
    string SourceAccountTitle,
    decimal SourceAmount,
    string SourceCurrency,
    Guid DestinationAccountId,
    string DestinationAccountTitle,
    decimal DestinationAmount,
    string DestinationCurrency,
    string? Description,
    DateTime OccurredAt
);