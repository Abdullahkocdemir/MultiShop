using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Results.AddressResults
{
    public class GetAddressByIdQueryResult
    {
        public int AddressId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
