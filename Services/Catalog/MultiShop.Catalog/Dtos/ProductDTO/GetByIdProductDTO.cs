namespace MultiShop.Catalog.Dtos.ProductDTO
{
    public class GetByIdProductDTO
    {
        public string? ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImageUrl { get; set; }
        public string? ProductDescription { get; set; }
        public string? CategoryId { get; set; }
    }
}
