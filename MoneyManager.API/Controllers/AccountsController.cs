using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.API.Contracts.UpdateRequests;
using MoneyManager.Application.Accounts.Commands.CreateAccount;
using MoneyManager.Application.Accounts.Commands.DeleteAccount;
using MoneyManager.Application.Accounts.Commands.UpdateAccount;
using MoneyManager.Application.Accounts.Queries;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount([FromBody] CreateAccountCommand command,  CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountRequest request ,CancellationToken ct)
    {
        var cmd = new UpdateAccountCommand(
            id,
            request.UserId,
            request.Title,
            request.Type,
            request.Currency,
            request.IsArchived
        );
        var result = await _mediator.Send(cmd,ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteAccountCommand(id), ct);
        return NoContent();
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAccounts(Guid userId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAccountsQuery(userId), ct);
        return Ok(result);
    }
}