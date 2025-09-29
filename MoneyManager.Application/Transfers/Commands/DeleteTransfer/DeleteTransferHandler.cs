using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Transfers.Commands.DeleteTransfer;

public class DeleteTransferHandler : IRequestHandler<DeleteTransferCommand, Unit>
{
    private readonly IWriteRepository<Transfer> _transferWriteRepository;
    private readonly IReadRepository<Transfer> _transferReadRepository;
    private readonly IWriteRepository<Account> _accountWriteRepository;
    private readonly IReadRepository<Account> _accountReadRepository;
    private readonly IUnitOfWork _uow;

    public DeleteTransferHandler(IWriteRepository<Transfer> transferWriteRepository,
        IReadRepository<Transfer> transferReadRepository,
        IWriteRepository<Account> accountWriteRepository,
        IReadRepository<Account> accountReadRepository,
        IUnitOfWork uow
        )
    {
        _transferWriteRepository = transferWriteRepository;
        _transferReadRepository = transferReadRepository;
        _accountWriteRepository = accountWriteRepository;
        _accountReadRepository = accountReadRepository;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteTransferCommand request, CancellationToken ct)
    {
        var transfer = await _transferReadRepository.GetByIdAsync(request.TransferId, ct)??
                       throw new NotFoundException("Transfer not found");
        var sourceAccount = await _accountReadRepository.GetByIdAsync(transfer.SourceAccountId, ct)??
                            throw new NotFoundException("Source account not found");
        var destinationAccount = await _accountReadRepository.GetByIdAsync(transfer.DestinationAccountId, ct)??
                                 throw new NotFoundException("Destination account not found");
        if(sourceAccount.UserId != transfer.UserId || destinationAccount.UserId != transfer.UserId)
            throw new ForbiddenException("Account does not belong to user.");
        sourceAccount.Balance += transfer.SourceAmount;
        destinationAccount.Balance -= transfer.DestinationAmount;
        _accountWriteRepository.Update(sourceAccount);
        _accountWriteRepository.Update(destinationAccount);
        _transferWriteRepository.Remove(transfer);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}