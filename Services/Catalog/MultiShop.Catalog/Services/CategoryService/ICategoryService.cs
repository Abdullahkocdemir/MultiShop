using MultiShop.Catalog.DTOs.CategoryDTO;

namespace MultiShop.Catalog.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDTO> GetByIdCategoryAsync(string id);
    }
}
