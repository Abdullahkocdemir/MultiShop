using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.ProductService;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProducttDetailFeatureViewComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        public _ProducttDetailFeatureViewComponentPartial( IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            return View(values);

        }
    }
}
