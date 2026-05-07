using MultiShop.DTOLayer.CatalogDTOs.BrandDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;

        public BrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultBrandDTO>> GetAllBrandAsync()
        {
            var response = await _httpClient.GetAsync("brands");
            return await response.Content.ReadFromJsonAsync<List<ResultBrandDTO>>();
        }

        public async Task CreateBrandAsync(CreateBrandDTO createBrandDTO)
        {
            await _httpClient.PostAsJsonAsync("brands", createBrandDTO);
        }

        public async Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO)
        {
            await _httpClient.PutAsJsonAsync("brands", updateBrandDTO);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _httpClient.DeleteAsync($"brands?id={id}");
        }

        public async Task<GetByIdBrandDTO> GetByIdBrandAsync(string id)
        {
            var response = await _httpClient.GetAsync($"brands/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdBrandDTO>();
        }
    }
}