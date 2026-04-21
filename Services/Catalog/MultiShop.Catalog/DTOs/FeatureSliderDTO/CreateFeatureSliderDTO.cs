namespace MultiShop.Catalog.DTOs.FeatureSliderDTO
{
    public class CreateFeatureSliderDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
