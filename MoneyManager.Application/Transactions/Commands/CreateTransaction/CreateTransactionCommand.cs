using MediatR;

namespace MoneyManager.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    Guid AccountId,
    Guid? SharedCategoryId,
    Guid? CustomCategoryId,
    decimal Amount,
    string? Description,
    DateTime OccurredAt) : IRequest<Guid>;
