using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Abstractions.Services;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, Guid>
{
    private readonly IWriteRepository<Transaction> _transactionRepositoryWrite;
    private readonly IReadRepository<Transaction> _transactionRepositoryRead;
    private readonly IWriteRepository<Account> _accountRepositoryWrite;
    private readonly IReadRepository<Account> _accountRepositoryRead;
    private readonly IUnitOfWork _uow;
    private readonly ICategoryLookup _category;
    
    public UpdateTransactionHandler(IWriteRepository<Transaction> repo,  
        IWriteRepository<Account> accountRepositoryWrite, 
        IUnitOfWork uow, IReadRepository<Account> accountRepositoryRead,
        ICategoryLookup categoryLookup, IReadRepository<Transaction> transactionRepositoryRead)
    {
        _transactionRepositoryWrite = repo;
        _uow = uow;
        _accountRepositoryWrite = accountRepositoryWrite;
        _accountRepositoryRead = accountRepositoryRead;
        _category = categoryLookup;
        _transactionRepositoryRead = transactionRepositoryRead;
    }
    
    public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken ct)
    {
        var transaction = await _transactionRepositoryRead.GetByIdAsync(request.Id, ct);
        if (transaction == null) throw new NotFoundException("Transaction not found");
        
        var oldAccount = await _accountRepositoryRead.GetByIdAsync(transaction.AccountId, ct);
        if (oldAccount == null) throw new NotFoundException("Account not found");
        
        var categoryType =
            await _category.GetCategoryTypeAsync(transaction.SharedCategoryId, transaction.CustomCategoryId, ct);
        var newCategoryType = await _category.GetCategoryTypeAsync(request.SharedCategoryId, request.CustomCategoryId, ct);
        if (newCategoryType != categoryType)
            throw new ConflictException("Category type doesn't match.");
        
        if (request.AccountId == oldAccount.Id)
        {
            if (categoryType == CategoryType.Expense)
                oldAccount.Balance = oldAccount.Balance + transaction.Amount - request.Amount;
            if (categoryType == CategoryType.Income)
                oldAccount.Balance = oldAccount.Balance + request.Amount - transaction.Amount;
        }
        else
        {
            var newAccount = await _accountRepositoryRead.GetByIdAsync(request.AccountId, ct);
            if (newAccount == null) 
                throw new NotFoundException("Account not found");
            if (newAccount.Currency != oldAccount.Currency) 
                throw new ConflictException("Accounts have different currencies");
            if (categoryType == CategoryType.Expense)
            {
                oldAccount.Balance += transaction.Amount;
                newAccount.Balance -=  request.Amount;
            }
            if (categoryType == CategoryType.Income)
            {
                oldAccount.Balance -= transaction.Amount;
                newAccount.Balance += request.Amount;
            }
            _accountRepositoryWrite.Update(newAccount);
        }
        
        transaction.AccountId = request.AccountId;
        transaction.SharedCategoryId = request.SharedCategoryId;
        transaction.CustomCategoryId = request.CustomCategoryId;
        transaction.Amount = request.Amount;
        transaction.Description = request.Description;
        transaction.OccurredAt = request.OccurredAt;
        
        _transactionRepositoryWrite.Update(transaction);
        _accountRepositoryWrite.Update(oldAccount);
        await _uow.SaveChangesAsync(ct);
        return transaction.Id;
    }
}