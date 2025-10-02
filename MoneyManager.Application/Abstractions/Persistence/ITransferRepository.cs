using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Abstractions.Persistence;

public interface ITransferRepository
{
    Task<IReadOnlyList<Transfer>> GetTransfersAsync(Guid userId, CancellationToken ct);
}