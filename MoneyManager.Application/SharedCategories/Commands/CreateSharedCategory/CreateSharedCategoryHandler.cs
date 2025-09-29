using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.SharedCategories.Commands.CreateSharedCategory;

public class CreateSharedCategoryHandler : IRequestHandler<CreateSharedCategoryCommand, Guid>
{
    private readonly IWriteRepository<SharedCategory> _categoryWrite;
    private readonly IUnitOfWork _uow;

    public CreateSharedCategoryHandler(IWriteRepository<SharedCategory> categoryWrite,
        IUnitOfWork uow)
    {
        _categoryWrite = categoryWrite;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateSharedCategoryCommand request, CancellationToken ct)
    {
        var category = new SharedCategory
        {
            Title = request.Title.ToLower(),
            Type = request.Type,
        };
        
        await _categoryWrite.AddAsync(category, ct);
        await _uow.SaveChangesAsync(ct);
        return category.Id;
    }
}