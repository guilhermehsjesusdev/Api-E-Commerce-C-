using ECommerce.Application.Common.Exceptions;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductRepository _productRepo;

    public CreateOrderHandler(IOrderRepository orderRepo, IProductRepository productRepo)
    {
        _orderRepo = orderRepo;
        _productRepo = productRepo;
    }

    public async Task<Guid> Handle(CreateOrderCommand cmd, CancellationToken ct)
    {
        var order = new Order(cmd.UserId);

        foreach (var item in cmd.Items)
        {
            var product = await _productRepo.GetByIdAsync(item.ProductId)
                ?? throw new NotFoundException("Product", item.ProductId);

            order.AddItem(product, item.Quantity);
            await _productRepo.UpdateAsync(product);
        }

        await _orderRepo.AddAsync(order);
        return order.Id;
    }
}