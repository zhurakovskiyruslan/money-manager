using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class SharedCategoryRepository :  ISharedCategoryRepository
{
    private readonly AppDbContext _context;
    public SharedCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<SharedCategory>> GetAllAsync(CancellationToken ct)
    {
        return await _context.SharedCategories.AsNoTracking().ToListAsync(ct);
    }
}