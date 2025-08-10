namespace MultiShop.Basket.Dtos
{
    public class BasketTotalDto
    {
        public string UserId { get; set; } = string.Empty;
        public string DiscountCode { get; set; } = string.Empty;
        public int? DiscountDto { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }

    }
}
