using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.CategoryService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _CategoriesDefaultViewComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _CategoriesDefaultViewComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}
