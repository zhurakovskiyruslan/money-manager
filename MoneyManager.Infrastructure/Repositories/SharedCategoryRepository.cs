using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Infrastructure.Repositories;

public class SharedCategoryRepository :  ISharedCategoryRepository
{
    private readonly AppDbContext _context;
    public SharedCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<SharedCategory>> GetAllAsync(CategoryType type, CancellationToken ct)
    {
        return await _context.SharedCategories
            .Where(c=>c.Type == type)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}