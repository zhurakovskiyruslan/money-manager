using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.API.Contracts.Accounts;
using MoneyManager.Application.Users.Commands.CreateUser;
using MoneyManager.Application.Users.Commands.DeleteUser;
using MoneyManager.Application.Users.Commands.UpdateUser;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        var cmd = new UpdateUserCommand(
            id,
            request.Name,
            request.Email,
            request.BaseCurrency,
            request.TimeZone
        );
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteUserCommand(id), ct);
        return NoContent();
    }
}