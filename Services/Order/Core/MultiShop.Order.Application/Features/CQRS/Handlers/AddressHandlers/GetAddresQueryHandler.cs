using MediatR;
using MultiShop.Order.Application.Entities;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddresQueryHandler : IRequestHandler<GetAddressQuery, List<GetAddresQueryResult>>
    {
        private readonly IRepository<Address> _repository;
        public GetAddresQueryHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAddresQueryResult>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAddresQueryResult
            {
                AddressId = x.AddressId,
                UserId = x.UserId,
                District = x.District,
                City = x.City,
                Country = x.Country,
                Detail = x.Detail
            }).ToList();
        }
    }
}
