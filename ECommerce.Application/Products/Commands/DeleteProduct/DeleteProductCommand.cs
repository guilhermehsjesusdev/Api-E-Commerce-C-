using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;