using MediatR;

namespace MoneyManager.Application.Transfers.Queries;

public record GetTransfersQuery(
    Guid UserId, 
    DateTimeOffset From, 
    DateTimeOffset To): IRequest<IEnumerable<TransferDto>>;