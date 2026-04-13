using MediatR;
using MultiShop.Order.Application.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderingHandlers
{
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;
        public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
        {
           var value = await _repository.GetByIdAsync(request.OrderingId);
            if (value == null)
            {
                throw new Exception("Ordering not found");
            }
            value.UserId = request.UserId;
            value.TotalPrice = request.TotalPrice;
            value.OrderDate = request.OrderDate;
            await _repository.UpdateAsync(value);
        }
    }
}
