using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.API.Contracts.UpdateRequests;
using MoneyManager.Application.CustomCategories.Commands.CreateCustomCategory;
using MoneyManager.Application.CustomCategories.Commands.DeleteCustomCategory;
using MoneyManager.Application.CustomCategories.Commands.UpdateCustomCategory;

namespace MoneyManager.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class CustomCategoriesController: ControllerBase
{
    private readonly IMediator _mediator;

    public CustomCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomCategory([FromBody] CreateCustomCategoryCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomCategory(Guid id,
        [FromBody] UpdateCustomCategoryRequest request, CancellationToken ct)
    {
        var cat = new UpdateCustomCategoryCommand(
            id,
            request.Title);
        var result = await _mediator.Send(cat, ct);
        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeactivateCustomCategory(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteCustomCategoryCommand(id), ct);
        return NoContent();
    }
}