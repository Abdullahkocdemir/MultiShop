using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;

namespace MultiShop.WebUI.Services.CatalogService.ProductService
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductAsync();
        Task<List<ResultProductWithCategoryDTO>> GetAllProductWithCategoryAsync();
        Task CreateProductAsync(CreateProductDTO createProductDTO);
        Task UpdateProductAsync(UpdateProductDTO updateProductDTO);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDTO> GetByIdProductAsync(string id);
        Task<List<ResultProductWithCategoryDTO>> IsFeatureList();
        Task<List<ResultProductWithCategoryDTO>> GetListCategoryOrProduct(string id);
    }
}
