using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTO;
using MultiShop.WebUI.Services.CatalogService.IProductDetailService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProductDetailInformationViewComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;
        public _ProductDetailInformationViewComponentPartial( IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.v1 = id;
            var values = await _productDetailService.GetByIdProductDetailAsync(id);
            return View(values);
        }
    }
}
