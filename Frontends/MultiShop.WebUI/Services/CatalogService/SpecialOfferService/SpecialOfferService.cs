using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.SpecialOfferService
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient _httpClient;

        public SpecialOfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync()
        {
            var response = await _httpClient.GetAsync("specialoffers");
            return await response.Content.ReadFromJsonAsync<List<ResultSpecialOfferDTO>>();
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO)
        {
            await _httpClient.PostAsJsonAsync("specialoffers", createSpecialOfferDTO);
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO)
        {
            await _httpClient.PutAsJsonAsync("specialoffers", updateSpecialOfferDTO);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _httpClient.DeleteAsync($"specialoffers?id={id}");
        }

        public async Task<GetByIdSpecialOfferDTO> GetByIdSpecialOfferAsync(string id)
        {
            var response = await _httpClient.GetAsync($"specialoffers/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdSpecialOfferDTO>();
        }
    }
}