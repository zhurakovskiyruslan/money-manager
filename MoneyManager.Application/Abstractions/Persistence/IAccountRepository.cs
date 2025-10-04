using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface IAccountRepository
{
    Task<IReadOnlyList<Account>> GetUserAccountsAsync(Guid userId, CancellationToken ct);
}