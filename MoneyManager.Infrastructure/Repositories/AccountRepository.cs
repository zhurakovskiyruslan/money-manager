using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class AccountRepository :  IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Account>> GetUserAccountsAsync(Guid userId, CancellationToken ct)
    {
        var accounts = await _context.Accounts
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);
        return accounts;
    }
}