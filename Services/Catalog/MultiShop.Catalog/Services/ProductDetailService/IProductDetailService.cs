using MultiShop.Catalog.DTOs.ProductDetailDTO;

namespace MultiShop.Catalog.Services.ProductDetailDetailService
{
    public interface IProductDetailDetailService
    {
        Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync();
        Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO);
        Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO);
        Task DeleteProductDetailAsync(string id);
        Task<GetByIdProductDetailDTO> GetByIdProductDetailAsync(string id);
        Task<GetByIdProductDetailDTO> GetByProductIdProductDetailAsync(string id);
    }
}
