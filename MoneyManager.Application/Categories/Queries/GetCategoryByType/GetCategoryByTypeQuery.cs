using MediatR;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Categories.Queries.GetCategoryByType;

public record GetCategoryByTypeQuery(CategoryType Type, Guid UserId): IRequest<List<CategoryDto>>;