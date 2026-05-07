using MultiShop.DTOLayer.CatalogDTOs.FeatureDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.FeatureService
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;

        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultFeatureDTO>> GetAllFeatureAsync()
        {
            var response = await _httpClient.GetAsync("features");
            return await response.Content.ReadFromJsonAsync<List<ResultFeatureDTO>>();
        }

        public async Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO)
        {
            await _httpClient.PostAsJsonAsync("features", createFeatureDTO);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO)
        {
            await _httpClient.PutAsJsonAsync("features", updateFeatureDTO);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _httpClient.DeleteAsync($"features?id={id}");
        }

        public async Task<GetByIdFeatureDTO> GetByIdFeatureAsync(string id)
        {
            var response = await _httpClient.GetAsync($"features/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdFeatureDTO>();
        }
    }
}