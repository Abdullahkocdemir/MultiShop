using MultiShop.DTOLayer.CatalogDTOs.ContactDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogService.ContactService
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultContactDTO>> GetAllContactAsync()
        {
            var response = await _httpClient.GetAsync("contacts");
            return await response.Content.ReadFromJsonAsync<List<ResultContactDTO>>();
        }

        public async Task CreateContactAsync(CreateContactDTO createContactDTO)
        {
            await _httpClient.PostAsJsonAsync("contacts", createContactDTO);
        }

        public async Task UpdateContactAsync(UpdateContactDTO updateContactDTO)
        {
            await _httpClient.PutAsJsonAsync("contacts", updateContactDTO);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync($"contacts?id={id}");
        }

        public async Task<GetByIdContactDTO> GetByIdContactAsync(string id)
        {
            var response = await _httpClient.GetAsync($"contacts/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdContactDTO>();
        }
    }
}