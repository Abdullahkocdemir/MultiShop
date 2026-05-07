using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTO;

namespace MultiShop.WebUI.Services.CatalogService.IProductDetailService
{
    public interface IProductDetailService
    {
        Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync();
        Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO);
        Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO);
        Task DeleteProductDetailAsync(string id);
        Task<GetByIdProductDetailDTO> GetByIdProductDetailAsync(string id);
        Task<GetByIdProductDetailDTO> GetByProductIdProductDetailAsync(string id);
    }
}
