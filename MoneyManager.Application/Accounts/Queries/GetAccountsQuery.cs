using MediatR;

namespace MoneyManager.Application.Accounts.Queries;

public record GetAccountsQuery(Guid UserId): IRequest<IEnumerable<AccountDto>>;