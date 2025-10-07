using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Categories.Queries;

public record CategoryDto(string Title, CategoryType Type, bool IsActive, CategorySource Source);