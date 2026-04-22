
using MultiShop.Catalog.DTOs.BrandDTO;

namespace MultiShop.Catalog.Services.BrandService
{
    public interface IBrandService
    {
        Task<List<ResultBrandDTO>> GetAllBrandAsync();
        Task CreateBrandAsync(CreateBrandDTO createBrandDTO);
        Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO);
        Task DeleteBrandAsync(string id);
        Task<GetByIdBrandDTO> GetByIdBrandAsync(string id);
    }
}
