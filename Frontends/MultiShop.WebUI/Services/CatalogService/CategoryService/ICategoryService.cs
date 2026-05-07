using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;

namespace MultiShop.WebUI.Services.CatalogService.CategoryService
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(string id);
        Task<GetByIdCategoryDTO> GetByIdCategoryAsync(string id);
        Task StatusChangeCategoryTrueAsync(string id);
        Task StatusChangeCategoryFalseAsync(string id);
    }
}
