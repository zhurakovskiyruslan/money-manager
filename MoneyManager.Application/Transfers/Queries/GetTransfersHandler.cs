using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
namespace MoneyManager.Application.Transfers.Queries;

public class GetTransfersHandler : IRequestHandler<GetTransfersQuery, IEnumerable<TransferDto>>
{
    private readonly ITransferRepository _repo;

    public GetTransfersHandler(ITransferRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TransferDto>> Handle(GetTransfersQuery request, CancellationToken ct)
    {
        var transfer = await _repo.GetTransfersAsync(
            request.UserId, request.From, request.To, ct);
        var response = transfer.Select(t => new TransferDto(
            t.SourceAccountId,
            t.SourceAccount.Title,
            t.SourceAmount,
            t.SourceAccount.Currency,
            t.DestinationAccountId,
            t.DestinationAccount.Title,
            t.DestinationAmount,
            t.DestinationAccount.Currency,
            t.Description,
            t.OccurredAt
        ));
        return response;
    }
}