using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.CustomCategories.Commands.CreateCustomCategory;

public class CreateCustomCategoryHandler : IRequestHandler<CreateCustomCategoryCommand, Guid>
{
    private readonly IWriteRepository<CustomCategory> _writeCategory;
    private readonly IUnitOfWork _uow;

    public CreateCustomCategoryHandler(
        IWriteRepository<CustomCategory> writeCategory,
        IUnitOfWork uow)
    {
        
        _writeCategory = writeCategory;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateCustomCategoryCommand request, CancellationToken ct)
    {
        var category = new CustomCategory
        {
            UserId = request.UserId,
            Title = request.Title,
            Type = request.Type
        };
        await _writeCategory.AddAsync(category, ct);
        await _uow.SaveChangesAsync(ct);
        return category.Id;
    }
}