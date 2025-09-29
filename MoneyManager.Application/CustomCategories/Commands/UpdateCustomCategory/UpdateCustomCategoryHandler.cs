using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.CustomCategories.Commands.UpdateCustomCategory;

public class UpdateCustomCategoryHandler : IRequestHandler<UpdateCustomCategoryCommand, Guid>
{
    private readonly IReadRepository<CustomCategory> _categoryRead;
    private readonly IWriteRepository<CustomCategory> _categoryWrite;
    private readonly IUnitOfWork _uow;

    public UpdateCustomCategoryHandler(IReadRepository<CustomCategory> categoryRead,
        IWriteRepository<CustomCategory> categoryWrite,
        IUnitOfWork uow)
    {
        _uow = uow;
        _categoryRead = categoryRead;
        _categoryWrite = categoryWrite;
    }

    public async Task<Guid> Handle(UpdateCustomCategoryCommand request, CancellationToken ct)
    {
        var category = await _categoryRead.GetByIdAsync(request.Id, ct)??
                       throw new NotFoundException("Category not found");
        if(!category.IsActive) throw new ConflictException("Category is not active");
        category.Title = request.Title.ToLower();
        _categoryWrite.Update(category);
        await _uow.SaveChangesAsync(ct);
        return request.Id;
    }
}