using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTO;

namespace MultiShop.WebUI.Services.CatalogService.ProductImageService
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultProductImageDTO>> GetAllProductImageAsync()
        {
            var response = await _httpClient.GetAsync("productimages");
            return await response.Content.ReadFromJsonAsync<List<ResultProductImageDTO>>();
        }

        public async Task<GetByIdProductImageDTO> GetByIdProductImageAsync(string id)
        {
            var response = await _httpClient.GetAsync($"productimages/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdProductImageDTO>();
        }

        public async Task<GetByIdProductImageDTO> GetByProductIdProductImageAsync(string id)
        {
            // API endpoint: GetProductImagesByProductId?id=...
            var response = await _httpClient.GetAsync($"productimages/GetProductImagesByProductId?id={id}");
            return await response.Content.ReadFromJsonAsync<GetByIdProductImageDTO>();
        }

        public async Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO)
        {
            await _httpClient.PostAsJsonAsync("productimages", createProductImageDTO);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO)
        {
            await _httpClient.PutAsJsonAsync("productimages", updateProductImageDTO);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _httpClient.DeleteAsync($"productimages/{id}");
        }
    }
}