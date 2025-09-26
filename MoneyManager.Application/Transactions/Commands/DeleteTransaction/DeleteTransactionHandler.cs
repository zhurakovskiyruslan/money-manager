using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Abstractions.Services;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly IWriteRepository<Transaction> _transactionRepositoryWrite;
    private readonly IReadRepository<Transaction> _transactionRepositoryRead;
    private readonly IWriteRepository<Account> _accountRepositoryWrite;
    private readonly IReadRepository<Account> _accountRepositoryRead;
    private readonly IUnitOfWork _uow;
    private readonly ICategoryLookup _category;

    public DeleteTransactionHandler(IWriteRepository<Transaction> transactionRepositoryWrite, 
        IReadRepository<Transaction> transactionRepositoryRead, IUnitOfWork uow,
        ICategoryLookup category,  IWriteRepository<Account> accountRepositoryWrite,
        IReadRepository<Account> accountRepositoryRead)
    {
        _transactionRepositoryWrite = transactionRepositoryWrite;
        _transactionRepositoryRead = transactionRepositoryRead;
        _uow = uow;
        _category = category;
        _accountRepositoryWrite = accountRepositoryWrite;
        _accountRepositoryRead = accountRepositoryRead;
    }


    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken ct)
    {
        
        var transaction = await _transactionRepositoryRead.GetByIdAsync(request.TransactionId, ct);
        if (transaction == null) throw new NotFoundException("Transaction not found");
        
        var account = await _accountRepositoryRead.GetByIdAsync(transaction.AccountId, ct);
        if (account == null) throw new NotFoundException("Transaction for account not found");
        var categoryType = await _category.GetCategoryTypeAsync(transaction.SharedCategoryId,
            transaction.CustomCategoryId, ct);
        if (categoryType == CategoryType.Expense)
        {
            account.Balance += transaction.Amount;;
        }
        if (categoryType == CategoryType.Income)
        {
            account.Balance -= transaction.Amount;
        }
        _accountRepositoryWrite.Update(account);
        _transactionRepositoryWrite.Remove(transaction);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}