using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.ProductImageService;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProductSliderImagerViewComponentPartial : ViewComponent
    {
        private readonly IProductImageService _productImageService;

        public _ProductSliderImagerViewComponentPartial(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            // Artık manuel URL yok, servis üzerinden Ocelot aracılığıyla veriyi çekiyoruz
            var values = await _productImageService.GetByProductIdProductImageAsync(id);
            return View(values);
        }
    }
}