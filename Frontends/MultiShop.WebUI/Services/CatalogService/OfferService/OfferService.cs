using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.OfferService
{
    public class OfferService : IOfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync()
        {
            var response = await _httpClient.GetAsync("offerdiscounts");
            return await response.Content.ReadFromJsonAsync<List<ResultOfferDiscountDTO>>();
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            await _httpClient.PostAsJsonAsync("offerdiscounts", createOfferDiscountDTO);
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            await _httpClient.PutAsJsonAsync("offerdiscounts", updateOfferDiscountDTO);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            // API query string beklediği için bu formatı koruyoruz
            await _httpClient.DeleteAsync($"offerdiscounts?id={id}");
        }

        public async Task<GetByIdOfferDiscountDTO> GetByIdOfferDiscountAsync(string id)
        {
            var response = await _httpClient.GetAsync($"offerdiscounts/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdOfferDiscountDTO>();
        }
    }
}