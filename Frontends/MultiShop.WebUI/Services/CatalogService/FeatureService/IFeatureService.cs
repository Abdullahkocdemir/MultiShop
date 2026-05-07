using MultiShop.DTOLayer.CatalogDTOs.FeatureDTO;

namespace MultiShop.WebUI.Services.CatalogService.FeatureService
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDTO>> GetAllFeatureAsync();
        Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO);
        Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO);
        Task DeleteFeatureAsync(string id);
        Task<GetByIdFeatureDTO> GetByIdFeatureAsync(string id);
    }
}
