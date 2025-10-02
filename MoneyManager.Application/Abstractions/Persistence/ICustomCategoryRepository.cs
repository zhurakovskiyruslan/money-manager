using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ICustomCategoryRepository
{
    Task<IReadOnlyList<CustomCategory>> GetAllCategoryByUserAsync(CategoryType type , Guid userId,  CancellationToken ct);
}