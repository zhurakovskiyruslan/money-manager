using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Abstractions.Services;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Services;

public class CategoryLookup : ICategoryLookup
{
    private readonly IReadRepository<SharedCategory> _sharedRepo;
    private readonly IReadRepository<CustomCategory>  _customRepo;
    

    public CategoryLookup(IReadRepository<SharedCategory> sharedRepo, 
        IReadRepository<CustomCategory> customRepo)
    {
        _sharedRepo = sharedRepo;
        _customRepo = customRepo;
    }
    
    public async Task<CategoryType> GetCategoryTypeAsync(Guid? sharedCategoryId, Guid? customCategoryId, CancellationToken ct)
    {
        if (sharedCategoryId.HasValue == customCategoryId.HasValue)
           throw new ConflictException("Exactly one of the categories must be provided.");
        
        if (sharedCategoryId != null)
        {
            return await Resolve<SharedCategory>(_sharedRepo, sharedCategoryId.Value, 
                c => c.IsActive, c => c.Type, ct);
        }
        else
        {
            return await Resolve<CustomCategory>(_customRepo, customCategoryId.Value, 
                c => c.IsActive, c => c.Type, ct);
        }
    }

    private static async Task<CategoryType> Resolve<T>(
        IReadRepository<T> repo, Guid id, Func<T, bool> isActive, Func<T, CategoryType> type,
        CancellationToken ct) where T : BaseEntity
    {
        var cat = await repo.GetByIdAsync(id, ct)
                  ?? throw new NotFoundException("Category not found");
        if (!isActive(cat)) throw new ConflictException("Category is not active");
        return type(cat);
    }

}