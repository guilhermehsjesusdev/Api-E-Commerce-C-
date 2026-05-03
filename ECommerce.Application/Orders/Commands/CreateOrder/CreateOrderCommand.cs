using MediatR;

namespace ECommerce.Application.Orders.Commands.CreateOrder;

public record OrderItemInput(Guid ProductId, int Quantity);

public record CreateOrderCommand(
    string UserId,
    List<OrderItemInput> Items
) : IRequest<Guid>;