using MediatR;
using MultiShop.Order.Application.Entities;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand>
    {
        private readonly IRepository<Address> _repository;
        public CreateAddressCommandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("Address bilgileri boş olamaz.");
            }
            else
            {
                Address address = new Address
                {
                    UserId = request.UserId,
                    District = request.District,
                    City = request.City,
                    Country = request.Country,
                    Detail = request.Detail
                };
                await _repository.CreateAsync(address);
            }
        }
    }
}
