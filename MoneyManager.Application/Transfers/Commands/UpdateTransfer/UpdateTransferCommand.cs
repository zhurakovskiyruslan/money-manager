using MediatR;

namespace MoneyManager.Application.Transfers.Commands.UpdateTransfer;

public record UpdateTransferCommand(
    Guid Id,
    Guid SourceAccountId,
    Guid DestinationAccountId,
    decimal SourceAmount,
    decimal DestinationAmount,
    string? Description,
    DateTime OccurredAt): IRequest<Guid>;