using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Abstractions.Services;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly IWriteRepository<Transaction> _transactionRepositoryWrite;
    private readonly IWriteRepository<Account> _accountRepositoryWrite;
    private readonly IReadRepository<Account> _accountRepositoryRead;
    private readonly IUnitOfWork _uow;
    private readonly ICategoryLookup _category;
   

    public CreateTransactionHandler(IWriteRepository<Transaction> repo,  
        IWriteRepository<Account> accountRepositoryWrite, 
        IUnitOfWork uow, IReadRepository<Account> accountRepositoryRead,
        ICategoryLookup categoryLookup)
    {
        _transactionRepositoryWrite = repo;
        _uow = uow;
        _accountRepositoryWrite = accountRepositoryWrite;
        _accountRepositoryRead = accountRepositoryRead;
       _category = categoryLookup;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken ct)
    {
        var account = await _accountRepositoryRead.GetByIdAsync(request.AccountId, ct);
        if (account == null) throw new NotFoundException("Account not found");
        if (account.IsArchived) throw new ConflictException("Account is already archived");
        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            SharedCategoryId = request.SharedCategoryId,
            CustomCategoryId = request.CustomCategoryId,
            Amount = request.Amount,
            Description = request.Description,
            OccurredAt = request.OccurredAt,
        };
        var categoryType = await _category.GetCategoryTypeAsync(transaction.SharedCategoryId, 
            transaction.CustomCategoryId, ct);
        if (categoryType == CategoryType.Expense)
        {
            account.Balance -= request.Amount;
        }
        if (categoryType == CategoryType.Income)
        {
            account.Balance += request.Amount;
        }
        await _transactionRepositoryWrite.AddAsync(transaction, ct);
        _accountRepositoryWrite.Update(account);
        await _uow.SaveChangesAsync(ct);
        return transaction.Id;
    }
}