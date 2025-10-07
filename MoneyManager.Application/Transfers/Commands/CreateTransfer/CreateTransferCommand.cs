using MediatR;

namespace MoneyManager.Application.Transfers.Commands.CreateTransfer;

public record CreateTransferCommand(
    Guid UserId,
    Guid SourceAccountId,
    Guid DestinationAccountId,
    decimal SourceAmount,
    decimal DestinationAmount,
    string? Description,
    DateTime OccurredAt
) :  IRequest<Guid>;