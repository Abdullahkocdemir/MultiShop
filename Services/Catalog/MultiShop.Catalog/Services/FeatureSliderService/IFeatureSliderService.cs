using MultiShop.Catalog.DTOs.FeatureSliderDTO;

namespace MultiShop.Catalog.Services.FeatureSliderService
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO);
        Task DeleteFeatureSliderAsync(string id);
        Task<GetByIdFeatureSliderDTO> GetByIdFeatureSliderAsync(string id);
        Task FeatureSliderChageStatusToTrue(string id);
        Task FeatureSliderChageStatusToFalse(string id);
    }
}
