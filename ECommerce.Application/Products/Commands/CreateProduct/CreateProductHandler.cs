using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repo;

    public CreateProductHandler(IProductRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateProductCommand cmd, CancellationToken ct)
    {
        var product = new Product(
            cmd.Name,
            cmd.Description,
            cmd.Price,
            cmd.Stock,
            cmd.CategoryId,
            cmd.ImageUrl
        );
        await _repo.AddAsync(product);
        return product.Id;
    }
}