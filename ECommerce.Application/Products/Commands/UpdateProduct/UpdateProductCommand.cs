using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl = null
) : IRequest;