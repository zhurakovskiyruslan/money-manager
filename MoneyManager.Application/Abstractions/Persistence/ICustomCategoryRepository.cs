using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ICustomCategoryRepository
{
    Task<IReadOnlyList<CustomCategory>> GetAllCategoryByUserAsync(Guid userId,  CancellationToken ct);
}