using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class EfReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    

    public EfReadRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<T>().FindAsync([id], ct).AsTask();
    }
}