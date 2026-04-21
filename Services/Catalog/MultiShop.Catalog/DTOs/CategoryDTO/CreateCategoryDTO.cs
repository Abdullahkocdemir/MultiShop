namespace MultiShop.Catalog.DTOs.CategoryDTO
{
    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
