using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class CreateOrderingCommand : IRequest<Unit>
    {
        public string UserId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
