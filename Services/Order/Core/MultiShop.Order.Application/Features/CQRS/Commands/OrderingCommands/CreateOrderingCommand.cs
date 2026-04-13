using MediatR;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderingCommands
{
    public class CreateOrderingCommand:IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
