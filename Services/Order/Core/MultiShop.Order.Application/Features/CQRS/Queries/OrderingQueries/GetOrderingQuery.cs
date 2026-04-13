using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderingResults;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderingQueries
{
    public class GetOrderingQuery : IRequest<List<GetOrderingQueryResult>>
    {
    }
}
