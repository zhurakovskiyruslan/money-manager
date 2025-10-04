using MediatR;

namespace MoneyManager.Application.Transactions.Queries;

public record GetTransactionsQuery(
    Guid UserId, 
    DateTimeOffset From, 
    DateTimeOffset To): IRequest<IEnumerable<TransactionDto>>;