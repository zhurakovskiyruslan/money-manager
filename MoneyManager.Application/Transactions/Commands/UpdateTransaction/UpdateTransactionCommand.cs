using MediatR;

namespace MoneyManager.Application.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(
    Guid Id,
    Guid AccountId,
    Guid? SharedCategoryId,
    Guid? CustomCategoryId,
    decimal Amount,
    string? Description,
    DateTime OccurredAt) : IRequest<Guid>;