namespace MultiShop.DTOLayer.BasketDTOs
{
    public class BasketItemDTO
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string ImageURL { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
