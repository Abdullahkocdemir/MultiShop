using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using MultiShop.WebUI.Services.CatalogService.CategoryService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutComponentPartials
{
    public class _NavbarUILayoutViewComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _NavbarUILayoutViewComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }
    }
}
