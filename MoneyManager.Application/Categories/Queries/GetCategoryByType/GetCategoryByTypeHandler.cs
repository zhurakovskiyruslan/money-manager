using MediatR;
using MoneyManager.Application.Abstractions.Persistence;

namespace MoneyManager.Application.Categories.Queries.GetCategoryByType;

public class GetCategoryByTypeHandler : IRequestHandler<GetCategoryByTypeQuery, List<CategoryDto>>
{
    private readonly ISharedCategoryRepository _sharedRepo;
    private readonly ICustomCategoryRepository _customRepo;
    
    public GetCategoryByTypeHandler(ISharedCategoryRepository sharedRepo, 
        ICustomCategoryRepository customRepo)
        {
        _sharedRepo = sharedRepo;
        _customRepo = customRepo;
        }

    public async Task<List<CategoryDto>> Handle(GetCategoryByTypeQuery request, CancellationToken ct)
    {
        var custom = await _customRepo.GetAllCategoryByUserAsync(request.Type, request.UserId, ct);
        var shared = await _sharedRepo.GetAllAsync(request.Type, ct);
        var customDto = custom.Select(c => new CategoryDto(
            c.Title,
            c.Type,
            c.IsActive,
            CategorySource.Custom
            ));

        var sharedDto = shared.Select(c => new CategoryDto
        (
            c.Title,
            c.Type,
            c.IsActive,
            CategorySource.Shared
        ));

        var categories = customDto.Concat(sharedDto).ToList();

        return categories;
    }
    
}