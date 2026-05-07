using MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.FeatureSliderService
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;

        public FeatureSliderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync()
        {
            var response = await _httpClient.GetAsync("featuresliders");
            return await response.Content.ReadFromJsonAsync<List<ResultFeatureSliderDTO>>();
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            await _httpClient.PostAsJsonAsync("featuresliders", createFeatureSliderDTO);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            await _httpClient.PutAsJsonAsync("featuresliders", updateFeatureSliderDTO);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _httpClient.DeleteAsync($"featuresliders?id={id}");
        }

        public async Task<GetByIdFeatureSliderDTO> GetByIdFeatureSliderAsync(string id)
        {
            var response = await _httpClient.GetAsync($"featuresliders/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdFeatureSliderDTO>();
        }

        public async Task FeatureSliderChageStatusToTrue(string id)
        {
            await _httpClient.GetAsync($"featuresliders/FeatureSliderChageStatusToTrue/{id}");
        }

        public async Task FeatureSliderChageStatusToFalse(string id)
        {
            await _httpClient.GetAsync($"featuresliders/FeatureSliderChageStatusToFalse/{id}");
        }
    }
}