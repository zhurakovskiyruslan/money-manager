using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Transfers.Commands.CreateTransfer;

public class CreateTransferHandler : IRequestHandler<CreateTransferCommand, Guid>
{
    private readonly IWriteRepository<Transfer> _transferRepositoryWrite;
    private readonly IWriteRepository<Account> _accountRepositoryWrite;
    private readonly IReadRepository<Account> _accountRepositoryRead;
    private readonly IUnitOfWork _uow;

    public CreateTransferHandler(IWriteRepository<Transfer> transferRepositoryWrite,
        IWriteRepository<Account> accountRepositoryWrite,
        IReadRepository<Account> accountRepositoryRead,
        IUnitOfWork uow)
    {
        _transferRepositoryWrite = transferRepositoryWrite;
        _accountRepositoryWrite = accountRepositoryWrite;
        _accountRepositoryRead = accountRepositoryRead;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateTransferCommand request, CancellationToken ct)
    {
        var sourceAccount = await _accountRepositoryRead.GetByIdAsync(request.SourceAccountId, ct)?? 
                            throw new NotFoundException("Source account not found");
        var destinationAccount = await _accountRepositoryRead.GetByIdAsync(request.DestinationAccountId, ct)??
                                 throw new NotFoundException("Destination account not found");
        if(sourceAccount.Balance < request.SourceAmount) 
            throw new ConflictException("Source account balance is less than amount of operation");
        if(sourceAccount.UserId != request.UserId || destinationAccount.UserId != request.UserId)
            throw new ForbiddenException("Account does not belong to user.");
        sourceAccount.Balance -= request.SourceAmount;
        destinationAccount.Balance += request.DestinationAmount;
        var transfer = new Transfer
        {
            UserId = request.UserId,
            SourceAccountId = request.SourceAccountId,
            DestinationAccountId = request.DestinationAccountId,
            SourceAmount = request.SourceAmount,
            DestinationAmount = request.DestinationAmount,
            FxRate = request.DestinationAmount/request.SourceAmount,
            Description = request.Description,
            OccurredAt = request.OccurredAt
        };
        _accountRepositoryWrite.Update(sourceAccount);
        _accountRepositoryWrite.Update(destinationAccount);
        await _transferRepositoryWrite.AddAsync(transfer, ct);
        await _uow.SaveChangesAsync(ct);
        return transfer.Id;
    }
}