using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Infrastructure.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly AppDbContext _context;

    public TransferRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Transfer>> GetTransfersAsync(Guid userId, CancellationToken ct)
    {
        return await _context.Transfers.Where(t=>t.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}