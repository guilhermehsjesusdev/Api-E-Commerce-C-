using ECommerce.Application.Common.Exceptions;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repo;

    public UpdateProductHandler(IProductRepository repo) => _repo = repo;

    public async Task Handle(UpdateProductCommand cmd, CancellationToken ct)
    {
        var product = await _repo.GetByIdAsync(cmd.Id)
            ?? throw new NotFoundException("Product", cmd.Id);

        product.Update(cmd.Name, cmd.Description, cmd.Price, cmd.ImageUrl);
        await _repo.UpdateAsync(product);
    }
}