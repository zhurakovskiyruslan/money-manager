using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly IWriteRepository<Transaction> _repo;
    private readonly IUnitOfWork _uow;

    public CreateTransactionHandler(IWriteRepository<Transaction> repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken ct)
    {
        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            SharedCategoryId = request.SharedCategoryId,
            CustomCategoryId = request.CustomCategoryId,
            Amount = request.Amount,
            Description = request.Description,
            OccurredAt = request.OccurredAt,
        };
        await _repo.AddAsync(transaction, ct);
        await _uow.SaveChangesAsync(ct);
        return transaction.Id;
    }
}