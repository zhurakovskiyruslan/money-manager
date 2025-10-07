using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.CustomCategories.Commands.DeleteCustomCategory;

public class DeleteCustomCategoryHandler : IRequestHandler<DeleteCustomCategoryCommand, Unit>
{
    private readonly IWriteRepository<CustomCategory> _categoryWrite;
    private readonly IReadRepository<CustomCategory> _categoryRead;
    private readonly IUnitOfWork _uow;

    public DeleteCustomCategoryHandler(IWriteRepository<CustomCategory> categoryWrite,
        IReadRepository<CustomCategory> categoryRead,
        IUnitOfWork uow)
    {
        _uow = uow;
        _categoryWrite = categoryWrite;
        _categoryRead = categoryRead;
    }

    public async Task<Unit> Handle(DeleteCustomCategoryCommand request, CancellationToken ct)
    {
        var category = await _categoryRead.GetByIdAsync(request.Id, ct)??
                       throw new NotFoundException("Category not found");
        if(!category.IsActive) return Unit.Value;
        category.IsActive = false;
        _categoryWrite.Update(category);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}