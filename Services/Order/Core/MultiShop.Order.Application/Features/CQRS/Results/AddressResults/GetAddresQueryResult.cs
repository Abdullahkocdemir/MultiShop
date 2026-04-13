namespace MultiShop.Order.Application.Features.CQRS.Results.AddressResults
{
    public class GetAddresQueryResult
    {
        public int AddressId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
