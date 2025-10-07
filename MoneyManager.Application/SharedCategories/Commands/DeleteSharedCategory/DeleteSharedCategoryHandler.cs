using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.SharedCategories.Commands.DeleteSharedCategory;

public class DeleteSharedCategoryHandler:IRequestHandler<DeleteSharedCategoryCommand, Unit>
{
    private readonly IWriteRepository<SharedCategory> _categoryWrite;
    private readonly IReadRepository<SharedCategory> _categoryRead;
    private readonly IUnitOfWork _uow;


    public DeleteSharedCategoryHandler(IWriteRepository<SharedCategory> categoryWrite, 
        IReadRepository<SharedCategory> categoryRead,
        IUnitOfWork uow)
    {
        _categoryWrite = categoryWrite;
        _categoryRead = categoryRead;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteSharedCategoryCommand request, CancellationToken ct)
    {
        var category = await _categoryRead.GetByIdAsync(request.CategoryId, ct)??
                       throw new NotFoundException("Category not found");
        category.IsActive = false;
        _categoryWrite.Update(category);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;

    }
}