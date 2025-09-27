using MediatR;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.SharedCategories.Commands.CreateSharedCategory;

public record CreateSharedCategoryCommand(
    string Title, 
    CategoryType Type
) :IRequest<Guid>;