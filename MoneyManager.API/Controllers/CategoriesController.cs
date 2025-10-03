

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.Categories.Queries;
using MoneyManager.Application.Categories.Queries.GetCategoryByType;

namespace MoneyManager.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator)
        {
        _mediator = mediator;
        }
    
    [HttpGet]
    public async Task<IActionResult> GetCategoriesByType([FromQuery] GetCategoryByTypeQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}