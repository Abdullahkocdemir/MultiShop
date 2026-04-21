namespace MultiShop.DTOLayer.CatalogDTOs.ProductDTO
{
    public class UpdateProductDTO
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public bool IsFeature { get; set; }
    }
}
