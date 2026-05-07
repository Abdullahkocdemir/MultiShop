using MultiShop.DTOLayer.CatalogDTOs.AboutDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.AboutService
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;

        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultAboutDTO>> GetAllAboutAsync()
        {
            var response = await _httpClient.GetAsync("abouts");
            return await response.Content.ReadFromJsonAsync<List<ResultAboutDTO>>();
        }

        public async Task CreateAboutAsync(CreateAboutDTO createAboutDTO)
        {
            await _httpClient.PostAsJsonAsync("abouts", createAboutDTO);
        }

        public async Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO)
        {
            await _httpClient.PutAsJsonAsync("abouts", updateAboutDTO);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync($"abouts?id={id}");
        }

        public async Task<GetByIdAboutDTO> GetByIdIAboutAsync(string id)
        {
            var response = await _httpClient.GetAsync($"abouts/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdAboutDTO>();
        }
    }
}