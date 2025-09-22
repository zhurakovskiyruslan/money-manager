using MoneyManager.Application.Abstractions.Persistence;

namespace MoneyManager.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public EfUnitOfWork(AppDbContext context)=>
        _context = context;

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
    _context.SaveChangesAsync(ct);
}