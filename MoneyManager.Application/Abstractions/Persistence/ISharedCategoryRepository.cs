using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ISharedCategoryRepository
{
    Task<IReadOnlyList<SharedCategory>> GetAllAsync(CategoryType type, CancellationToken ct);
}