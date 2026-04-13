using MediatR;
using MultiShop.Order.Application.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand>
    {
        private readonly IRepository<OrderDetail> _repository;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("Gelen Değerler Boş Veya Eksik");
            }
            else
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    Amount = request.Amount,
                    Name = request.Name,
                    Price = request.Price,
                    TotalPrice = request.TotalPrice,
                    OrderingId = request.OrderingId,
                    ProductId = request.ProductId,

                };
                await _repository.CreateAsync(orderDetail);
            }

        }
    }
}
