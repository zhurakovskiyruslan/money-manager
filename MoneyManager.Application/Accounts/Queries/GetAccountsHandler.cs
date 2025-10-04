using MediatR;
using MoneyManager.Application.Abstractions.Persistence;

namespace MoneyManager.Application.Accounts.Queries;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _repo;

    public GetAccountsHandler(IAccountRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken ct)
    {
        var accounts =  await _repo.GetUserAccountsAsync(request.UserId, ct);
        var result = accounts.Select(a => new AccountDto(
            a.Title,
            a.Type,
            a.Currency,
            a.Balance
        ));
        return result;
    }
}