using ECommerce.Application.Products.Commands.CreateProduct;
using ECommerce.Application.Products.Commands.DeleteProduct;
using ECommerce.Application.Products.Commands.UpdateProduct;
using ECommerce.Application.Products.Queries.GetAllProducts;
using ECommerce.Application.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _mediator.Send(new GetAllProductsQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await _mediator.Send(new GetProductByIdQuery(id)));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        await _mediator.Send(new UpdateProductCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.ImageUrl
        ));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl = null
);