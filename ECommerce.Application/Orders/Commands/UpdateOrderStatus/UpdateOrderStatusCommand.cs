using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus NewStatus) : IRequest;