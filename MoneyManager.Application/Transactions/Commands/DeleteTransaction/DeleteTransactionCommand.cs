using MediatR;

namespace MoneyManager.Application.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid TransactionId) : IRequest<Unit>;