using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.SharedCategories.Commands.DeleteSharedCategory;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SharedCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SharedCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]

public async Task<IActionResult> DeactivateCustomCategory(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteSharedCategoryCommand(id), ct);
        return NoContent();
    }
}