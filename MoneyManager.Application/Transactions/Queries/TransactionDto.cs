using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Transactions.Queries;

public record TransactionDto(
    CategoryType Category,
    decimal Amount,
    string? Description,
    DateTime OccuredAt
);