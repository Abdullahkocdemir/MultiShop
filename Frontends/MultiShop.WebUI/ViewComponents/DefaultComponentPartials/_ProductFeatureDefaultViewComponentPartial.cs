using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.ProductService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _ProductFeatureDefaultViewComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        public _ProductFeatureDefaultViewComponentPartial(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productIfeatureList = await _productService.IsFeatureList();
            return View(productIfeatureList);

        }
    }
}
