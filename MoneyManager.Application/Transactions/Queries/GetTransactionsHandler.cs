using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.Application.Transactions.Queries;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, 
    IEnumerable<TransactionDto>>
{
    private readonly ITransactionRepository _repo;

    public GetTransactionsHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken ct)
    {
        var transactions = await _repo.
            GetUserTransactionsAsync(request.UserId, request.From, request.To, ct);

        var response = (from transaction in transactions where !transaction.IsDeleted 
            select new TransactionDto(
                GetType(transaction), 
                transaction.Amount, 
                transaction.Description, 
                transaction.OccurredAt)).ToList();
        return response;
    }

    private CategoryType GetType(Transaction transaction)
    {
        if (transaction.CustomCategoryId != null)
            return transaction.CustomCategory!.Type;
        else
            return transaction.SharedCategory!.Type;
    }
    
    
}