using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ISharedCategoryRepository
{
    Task<IReadOnlyList<SharedCategory>> GetAllAsync(CancellationToken ct);
}