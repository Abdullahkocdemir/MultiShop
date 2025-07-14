using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Results.OrderingResults
{
    public class GetOrderingByIdQueryResult
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
