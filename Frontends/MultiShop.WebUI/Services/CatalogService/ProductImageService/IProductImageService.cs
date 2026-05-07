using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTO;

namespace MultiShop.WebUI.Services.CatalogService.ProductImageService
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDTO>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO);
        Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO);
        Task DeleteProductImageAsync(string id);
        Task<GetByIdProductImageDTO> GetByIdProductImageAsync(string id);
        Task<GetByIdProductImageDTO> GetByProductIdProductImageAsync(string id);
    }
}
