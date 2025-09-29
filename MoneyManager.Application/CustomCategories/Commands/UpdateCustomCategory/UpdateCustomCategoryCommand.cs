using MediatR;

namespace MoneyManager.Application.CustomCategories.Commands.UpdateCustomCategory;

public record UpdateCustomCategoryCommand(Guid Id, string Title): IRequest<Guid>;