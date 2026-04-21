namespace MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTO
{
    public class GetByIdFeatureSliderDTO
    {
        public string FeatureSliderId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
