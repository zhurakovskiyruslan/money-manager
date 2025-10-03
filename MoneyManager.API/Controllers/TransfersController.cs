using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.API.Contracts.UpdateRequests;
using MoneyManager.Application.Transfers.Commands.CreateTransfer;
using MoneyManager.Application.Transfers.Commands.DeleteTransfer;
using MoneyManager.Application.Transfers.Commands.UpdateTransfer;
using MoneyManager.Application.Transfers.Queries;

namespace MoneyManager.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransfersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateTransfer(Guid id, [FromBody] UpdateTransferRequest request,
        CancellationToken ct)
    {
        var command = new UpdateTransferCommand(
            id,
            request.SourceAccountId,
            request.DestinationAccountId,
            request.SourceAmount,
            request.DestinationAmount,
            request.Description,
            request.OccurredAt
        );
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteTransfer(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteTransferCommand(id), ct);
        return NoContent();
    }

    [HttpGet("{userId:Guid}")]
    public async Task<IActionResult> GetTransfer(Guid userId, 
        [FromQuery] DateTimeOffset from, DateTimeOffset to, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTransfersQuery(userId, from, to), ct);
        return Ok(result);
    }
}