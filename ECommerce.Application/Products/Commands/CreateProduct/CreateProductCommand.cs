using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    Guid CategoryId,
    string? ImageUrl = null
) : IRequest<Guid>;