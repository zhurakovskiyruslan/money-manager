using MediatR;

namespace MoneyManager.Application.SharedCategories.Commands.DeleteSharedCategory;

public record DeleteSharedCategoryCommand(Guid CategoryId): IRequest<Unit>;