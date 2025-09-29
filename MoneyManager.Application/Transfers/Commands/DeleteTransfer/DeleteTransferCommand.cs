using MediatR;

namespace MoneyManager.Application.Transfers.Commands.DeleteTransfer;

public record DeleteTransferCommand(Guid TransferId) : IRequest<Unit>;