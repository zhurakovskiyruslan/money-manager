using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Transfers.Commands.UpdateTransfer;

public class UpdateTransferHandler : IRequestHandler<UpdateTransferCommand, Guid>
{
    private readonly IReadRepository<Transfer> _transferReadRepository;
    private readonly IWriteRepository<Transfer> _transferWriteRepository;
    private readonly IReadRepository<Account> _accountReadRepository;
    private readonly IWriteRepository<Account> _accountWriteRepository;
    private readonly IUnitOfWork _uow;

    public UpdateTransferHandler(IReadRepository<Transfer> transferReadRepository,
        IWriteRepository<Transfer> transferWriteRepository,
        IReadRepository<Account> accountReadRepository,
        IWriteRepository<Account> accountWriteRepository,
        IUnitOfWork uow)
    {
        _transferReadRepository = transferReadRepository;
        _transferWriteRepository = transferWriteRepository;
        _accountReadRepository = accountReadRepository;
        _accountWriteRepository = accountWriteRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(UpdateTransferCommand request, CancellationToken ct)
    {
        var transfer = await _transferReadRepository.GetByIdAsync(request.Id, ct) ??
                       throw new NotFoundException("Transfer not found.");
        
        var oldSourceAccount = await _accountReadRepository.GetByIdAsync(transfer.SourceAccountId, ct) ??
                               throw new NotFoundException("Source account not found.");
        var oldDestinationAccount = await _accountReadRepository.GetByIdAsync(transfer.DestinationAccountId, ct) ??
                                    throw new NotFoundException("Destination account not found.");

        var newSourceAccount = transfer.SourceAccountId == request.SourceAccountId? oldSourceAccount : 
            await _accountReadRepository.GetByIdAsync(request.SourceAccountId, ct) ??
                         throw new NotFoundException("Source account not found.");
        var newDestinationAccount = transfer.DestinationAccountId == request.DestinationAccountId? oldDestinationAccount :
            await _accountReadRepository.GetByIdAsync(request.DestinationAccountId, ct) ??
                                    throw new NotFoundException("Destination account not found.");
        oldSourceAccount.Balance += transfer.SourceAmount;
        oldDestinationAccount.Balance -= transfer.DestinationAmount;
        
        newSourceAccount.Balance -= request.SourceAmount;
        newDestinationAccount.Balance += request.DestinationAmount;
        
        transfer.SourceAccountId = request.SourceAccountId;
        transfer.DestinationAccountId = request.DestinationAccountId;
        transfer.DestinationAmount = request.DestinationAmount;
        transfer.SourceAmount = request.SourceAmount;
        transfer.Description = request.Description;
        transfer.OccurredAt = request.OccurredAt;
        transfer.UpdatedAt = DateTime.UtcNow;
        transfer.FxRate = request.DestinationAmount/request.SourceAmount;
        
        _accountWriteRepository.Update(oldSourceAccount);
        if(!ReferenceEquals(newSourceAccount, oldSourceAccount))
            _accountWriteRepository.Update(newSourceAccount);
        _accountWriteRepository.Update(oldDestinationAccount);
        if(!ReferenceEquals(newDestinationAccount, oldDestinationAccount))
            _accountWriteRepository.Update(newDestinationAccount);
        
        _transferWriteRepository.Update(transfer);
        await _uow.SaveChangesAsync(ct);
        
        return transfer.Id;
    }
}