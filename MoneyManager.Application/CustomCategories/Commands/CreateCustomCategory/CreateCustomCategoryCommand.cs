using MediatR;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.CustomCategories.Commands.CreateCustomCategory;

public record CreateCustomCategoryCommand(
    Guid UserId,
    string Title,
    CategoryType Type
): IRequest<Guid>;