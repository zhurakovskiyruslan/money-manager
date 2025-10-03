using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetUserTransactionsAsync(Guid userId, 
        DateTimeOffset from, DateTimeOffset to, CancellationToken ct);
    Task<IReadOnlyList<Transaction>> GetAccountTransactionsAsync(Guid accountId, CancellationToken ct);
}