using ECommerce.Application.Orders.Dtos;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetOrdersByUser;

public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repo;

    public GetOrdersByUserHandler(IOrderRepository repo) => _repo = repo;

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByUserQuery q, CancellationToken ct)
    {
        var orders = await _repo.GetByUserIdAsync(q.UserId);

        return orders.Select(o => new OrderDto(
            o.Id,
            o.UserId,
            o.Status,
            o.Total,
            o.CreatedAt,
            o.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.Product?.Name ?? string.Empty,
                i.UnitPrice,
                i.Quantity,
                i.Total
            )).ToList()
        ));
    }
}