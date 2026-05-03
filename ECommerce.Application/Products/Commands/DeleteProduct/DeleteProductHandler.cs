using ECommerce.Application.Common.Exceptions;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repo;

    public DeleteProductHandler(IProductRepository repo) => _repo = repo;

    public async Task Handle(DeleteProductCommand cmd, CancellationToken ct)
    {
        var product = await _repo.GetByIdAsync(cmd.Id)
            ?? throw new NotFoundException("Product", cmd.Id);

        await _repo.DeleteAsync(cmd.Id);
    }
}