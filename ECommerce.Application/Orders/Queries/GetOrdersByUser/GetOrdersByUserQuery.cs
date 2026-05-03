using ECommerce.Application.Orders.Dtos;
using MediatR;

namespace ECommerce.Application.Orders.Queries.GetOrdersByUser;

public record GetOrdersByUserQuery(string UserId) : IRequest<IEnumerable<OrderDto>>;