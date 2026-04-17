namespace MultiShop.Basket.Dtos
{
    public class BasketTotalDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string DiscountCode { get; set; } = string.Empty;
        public int DiscountRate { get; set; }
        public List<BasketItemDTO> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems.Sum(w => w.Price * w.Quantity); }
    }
}
