using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface IWriteRepository<T> where T: BaseEntity
{
    Task AddAsync(T entity, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
    void Update(T entity);
    void Remove(T entity);
}