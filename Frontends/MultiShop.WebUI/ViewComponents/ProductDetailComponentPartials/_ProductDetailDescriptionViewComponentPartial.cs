using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.IProductDetailService;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProductDetailDescriptionViewComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;
        public _ProductDetailDescriptionViewComponentPartial( IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        public async Task< IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.v1 = id;
            var values = await _productDetailService.GetByIdProductDetailAsync(id);
            return View(values);
        }
    }
}
