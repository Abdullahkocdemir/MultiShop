using MediatR;
using MultiShop.Order.Application.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand>
    {
        private readonly IRepository<OrderDetail> _repository;
        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.OrderingId);
            if (value == null)
            {
                throw new Exception("OrderDetail not found");
            }
            else
            {
                value.ProductId = request.ProductId;
                value.Name = request.Name;
                value.Price = request.Price;
                value.Amount = request.Amount;
                value.TotalPrice = request.TotalPrice;
                await _repository.UpdateAsync(value);
            }
        }
    }
}
