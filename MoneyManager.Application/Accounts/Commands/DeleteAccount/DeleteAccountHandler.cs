using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IWriteRepository<Account> _write;
    private readonly IReadRepository<Account> _read;
    private readonly IUnitOfWork _uow;

    public DeleteAccountHandler(IWriteRepository<Account> write, 
        IReadRepository<Account> read,  IUnitOfWork uow)
    {
        _write = write;
        _read = read;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        var account = await _read.GetByIdAsync(request.Id, ct);
        if (account == null) throw new Exception($"{request.Id} not found");
        _write.Remove(account);
        await _uow.SaveChangesAsync(ct);
        return Unit.Value;
    }
}