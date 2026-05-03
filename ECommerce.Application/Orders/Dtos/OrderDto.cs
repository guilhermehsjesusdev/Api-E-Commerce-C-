using ECommerce.Domain.Enums;

namespace ECommerce.Application.Orders.Dtos;

public record OrderDto(
    Guid Id,
    string UserId,
    OrderStatus Status,
    decimal Total,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);