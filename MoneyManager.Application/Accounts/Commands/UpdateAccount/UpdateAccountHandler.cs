using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Application.Common.Exceptions;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly IWriteRepository<Account> _writeRepo;
    private readonly IReadRepository<Account> _readRepo;
    private readonly IUnitOfWork _uow;

    public UpdateAccountHandler(IWriteRepository<Account> writeRepo, IReadRepository<Account> readRepo, IUnitOfWork uow)
    {
        _writeRepo = writeRepo;
        _readRepo = readRepo;
        _uow = uow;
    }
    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken ct)
    {
        var account = await _readRepo.GetByIdAsync(request.Id, ct);
        if (account == null)
            throw new NotFoundException("Account not found");
        if (account.UserId != request.UserId)
            throw new ForbiddenException("No access to this account.");
        account.Title = request.Title;
        account.Type = request.Type;
        account.Currency = request.Currency;
        account.IsArchived = request.IsArchived;
        
        _writeRepo.Update(account);
        await _uow.SaveChangesAsync(ct);
        return account.Id;
    }
}