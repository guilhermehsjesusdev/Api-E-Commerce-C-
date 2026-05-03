using ECommerce.Application.Orders.Commands.CreateOrder;
using ECommerce.Application.Orders.Commands.UpdateOrderStatus;
using ECommerce.Application.Orders.Queries.GetOrdersByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await _mediator.Send(new GetOrdersByUserQuery(userId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var cmd = new CreateOrderCommand(userId, request.Items);
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetMyOrders), new { id }, new { id });
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusCommand cmd)
    {
        await _mediator.Send(cmd with { OrderId = id });
        return NoContent();
    }
}

public record CreateOrderRequest(List<OrderItemInput> Items);