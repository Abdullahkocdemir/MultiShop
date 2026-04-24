using MultiShop.Catalog.DTOs.AboutDTO;

namespace MultiShop.Catalog.Services.AboutService
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
