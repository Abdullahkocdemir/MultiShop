using MultiShop.Catalog.DTOs.FeatureDTO;

namespace MultiShop.Catalog.Services.FeatureService
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
