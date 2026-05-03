using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Products.Dtos;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repo;

    public GetProductByIdHandler(IProductRepository repo) => _repo = repo;

    public async Task<ProductDto> Handle(GetProductByIdQuery q, CancellationToken ct)
    {
        var product = await _repo.GetByIdAsync(q.Id)
            ?? throw new NotFoundException("Product", q.Id);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CategoryId,
            product.ImageUrl
        );
    }
}