namespace MultiShop.DTOLayer.CatalogDTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
