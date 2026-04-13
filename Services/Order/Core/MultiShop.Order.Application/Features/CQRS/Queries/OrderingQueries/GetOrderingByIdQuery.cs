using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderingResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries
{
    public class GetOrderingByIdQuery : IRequest<GetOrderingByIdQueryResult>
    {
        public GetOrderingByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
