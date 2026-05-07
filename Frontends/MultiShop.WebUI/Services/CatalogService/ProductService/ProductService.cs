using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            var response = await _httpClient.GetAsync("products");
            return await response.Content.ReadFromJsonAsync<List<ResultProductDTO>>();
        }

        public async Task<List<ResultProductWithCategoryDTO>> GetAllProductWithCategoryAsync()
        {
            var response = await _httpClient.GetAsync("products/ProductListWithCategory");
            return await response.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDTO>>();
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            await _httpClient.PostAsJsonAsync("products", createProductDTO);
        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            await _httpClient.PutAsJsonAsync("products", updateProductDTO);
        }

        public async Task DeleteProductAsync(string id)
        {
            // API tarafında [HttpDelete] ID'yi query string mi yoksa route'tan mı alıyor? 
            // Query string ise: products?id={id} kullanmalısın.
            await _httpClient.DeleteAsync($"products?id={id}");
        }

        public async Task<GetByIdProductDTO> GetByIdProductAsync(string id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdProductDTO>();
        }

        public async Task<List<ResultProductWithCategoryDTO>> IsFeatureList()
        {
            var response = await _httpClient.GetAsync("products/IsFeatureList");
            return await response.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDTO>>();
        }

        public async Task<List<ResultProductWithCategoryDTO>> GetListCategoryOrProduct(string id)
        {
            var response = await _httpClient.GetAsync($"products/GetListCategoryOrProduct/{id}");
            return await response.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDTO>>();
        }
    }
}