using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Abstractions.Services;

public interface ICategoryLookup
{
    public Task<CategoryType> GetCategoryTypeAsync(
        Guid? sharedCategoryId,
        Guid? customCategoryId,
        CancellationToken ct
    );
}