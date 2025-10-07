using MediatR;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Accounts.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IWriteRepository<Account> _repo;
    private readonly IUnitOfWork _uow;

    public CreateAccountHandler(IWriteRepository<Account> repo,  IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }
    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken ct)
    {
        var account = new Account
        {
            UserId = request.UserId,
            Title = request.Title,
            Type = request.Type,
            Currency = request.Currency,
            Balance = request.Balance
        };
        await _repo.AddAsync(account, ct);
        await _uow.SaveChangesAsync(ct);
        return account.Id;
    }
}