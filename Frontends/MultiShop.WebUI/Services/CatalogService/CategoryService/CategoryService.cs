using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;

namespace MultiShop.WebUI.Services.CatalogService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            return await response.Content.ReadFromJsonAsync<List<ResultCategoryDTO>>();
        }

        public async Task<GetByIdCategoryDTO> GetByIdCategoryAsync(string id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdCategoryDTO>();
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            await _httpClient.PostAsJsonAsync("categories", createCategoryDTO);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            await _httpClient.PutAsJsonAsync("categories", updateCategoryDTO);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync($"categories/{id}");
        }

        // Opsiyonel: Diğer metotları da benzer şekilde doldurabilirsiniz.
        public Task StatusChangeCategoryFalseAsync(string id) => throw new NotImplementedException();
        public Task StatusChangeCategoryTrueAsync(string id) => throw new NotImplementedException();
    }
}