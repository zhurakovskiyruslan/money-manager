using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Infrastructure.Repositories;

public class CustomCategoryRepository : ICustomCategoryRepository
{
    private readonly AppDbContext _context;
    public CustomCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<CustomCategory>> GetAllCategoryByUserAsync(CategoryType type, Guid userId, 
         CancellationToken ct)
    {
        return await _context.CustomCategories
            .Where(c=>c.Type == type)
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}