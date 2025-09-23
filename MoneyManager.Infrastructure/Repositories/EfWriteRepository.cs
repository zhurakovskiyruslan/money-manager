using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class EfWriteRepository<T> : IWriteRepository<T> where T: BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfWriteRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public Task AddAsync(T entity, CancellationToken ct = default) => 
        _dbSet.AddAsync(entity, ct).AsTask();


    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default) =>
        _dbSet.AddRangeAsync(entities, ct);

    public void Update(T entity) => 
        _dbSet.Update(entity);

    public void Remove (T entity) =>
        _dbSet.Remove(entity);
}