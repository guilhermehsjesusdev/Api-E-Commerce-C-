using ECommerce.Application.Products.Dtos;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repo;

    public GetAllProductsHandler(IProductRepository repo) => _repo = repo;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery q, CancellationToken ct)
    {
        var products = await _repo.GetAllAsync();
        return products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.Stock,
            p.CategoryId,
            p.ImageUrl
        ));
    }
}