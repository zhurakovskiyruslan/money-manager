using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.API.Contracts.UpdateRequests;
using MoneyManager.Application.Transactions.Commands.CreateTransaction;
using MoneyManager.Application.Transactions.Commands.DeleteTransaction;
using MoneyManager.Application.Transactions.Commands.UpdateTransaction;
using MoneyManager.Application.Transactions.Queries;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{   
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionRequest req, CancellationToken ct)
    {
        var cmd = new UpdateTransactionCommand(
            id,
            req.AccountId,
            req.SharedCategoryId,
            req.CustomCategoryId,
            req.Amount,
            req.Description,
            req.OccurredAt
        );
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteTransactionCommand(id), ct);
        return NoContent();
    }

    [HttpGet("{userId:Guid}")]
    public async Task<IActionResult> GetTransactions(Guid userId, [FromQuery] DateTimeOffset from, DateTimeOffset to, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTransactionsQuery(userId, from, to), ct);
        return Ok(result);
    }
}