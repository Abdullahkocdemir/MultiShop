using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTO;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogService.IProductDetailService
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync()
        {
            var response = await _httpClient.GetAsync("productdetails");
            return await response.Content.ReadFromJsonAsync<List<ResultProductDetailDTO>>();
        }

        public async Task<GetByIdProductDetailDTO> GetByIdProductDetailAsync(string id)
        {
            var response = await _httpClient.GetAsync($"productdetails/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    return new GetByIdProductDetailDTO(); 
                }

                return JsonConvert.DeserializeObject<GetByIdProductDetailDTO>(content);
            }

            return new GetByIdProductDetailDTO();
        }

        public async Task<GetByIdProductDetailDTO> GetByProductIdProductDetailAsync(string id)
        {
            // Controller'daki endpoint yapısına göre: GetProductDetailByProductId?id=...
            var response = await _httpClient.GetAsync($"productdetails/GetProductDetailByProductId?id={id}");
            return await response.Content.ReadFromJsonAsync<GetByIdProductDetailDTO>();
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO)
        {
            await _httpClient.PostAsJsonAsync("productdetails", createProductDetailDTO);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO)
        {
            await _httpClient.PutAsJsonAsync("productdetails", updateProductDetailDTO);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _httpClient.DeleteAsync($"productdetails?id={id}");
        }
    }
}