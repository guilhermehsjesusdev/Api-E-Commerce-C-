using ECommerce.Application.Common.Exceptions;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand>
{
    private readonly IOrderRepository _repo;

    public UpdateOrderStatusHandler(IOrderRepository repo) => _repo = repo;

    public async Task Handle(UpdateOrderStatusCommand cmd, CancellationToken ct)
    {
        var order = await _repo.GetByIdAsync(cmd.OrderId)
            ?? throw new NotFoundException("Product", cmd.OrderId);

        if (cmd.NewStatus == OrderStatus.Confirmed) order.Confirm();
        else if (cmd.NewStatus == OrderStatus.Cancelled) order.Cancel();

        await _repo.UpdateAsync(order);
    }
}