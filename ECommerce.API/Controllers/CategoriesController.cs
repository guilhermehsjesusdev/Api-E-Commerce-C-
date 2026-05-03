using ECommerce.Application.Categories.Commands.CreateCategory;
using ECommerce.Application.Categories.Queries.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _mediator.Send(new GetAllCategoriesQuery()));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetAll), new { id }, new { id });
    }
}