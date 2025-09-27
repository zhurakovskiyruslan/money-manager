using MediatR;

namespace MoneyManager.Application.CustomCategories.Commands.DeleteCustomCategory;

public record DeleteCustomCategoryCommand(Guid Id ) : IRequest<Unit>;