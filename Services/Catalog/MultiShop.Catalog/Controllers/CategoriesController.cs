using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDTO;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryServices.GetAllCategoryAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCategoryId(string id)
        {
            var values = await _categoryServices.GetByIdCategoryAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            await _categoryServices.CreateCategoryAsync(createCategoryDTO);
            return Ok("Kategori Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Kategori Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryServices.UpdateCategoryAsync(updateCategoryDTO);
            return Ok("Kategori Başarılı Bir Şekilde Güncellendi");
        }
    }
}
