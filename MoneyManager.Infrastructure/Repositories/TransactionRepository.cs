using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Transaction>> GetUserTransactionsAsync(Guid userId, 
        DateTimeOffset from, DateTimeOffset to, CancellationToken ct)
    {
        return await _context.Transactions
            .Where(t=> t.OccurredAt >= from && t.OccurredAt <= to)
            .Include(t=> t.Account)
            .Where(t=> t.Account.UserId == userId)
            .Include(t=>t.SharedCategory)
            .Include(t => t.CustomCategory)
            .AsNoTracking()
            .ToListAsync(ct);
        
    }

    public async Task<IReadOnlyList<Transaction>> GetAccountTransactionsAsync(Guid accountId, CancellationToken ct)
    {
        return await _context.Transactions.Where(t=> t.AccountId == accountId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}