using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderingCommands
{
    public class RemoveOrderingCommand : IRequest
    {
        public RemoveOrderingCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
