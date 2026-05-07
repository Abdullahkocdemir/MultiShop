using MultiShop.DTOLayer.CatalogDTOs.AboutDTO;

namespace MultiShop.WebUI.Services.CatalogService.AboutService
{
    public interface IAboutService
    {
        Task<List<ResultAboutDTO>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDTO createAboutDTO);
        Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO);
        Task DeleteAboutAsync(string id);
        Task<GetByIdAboutDTO> GetByIdIAboutAsync(string id);
    }
}
