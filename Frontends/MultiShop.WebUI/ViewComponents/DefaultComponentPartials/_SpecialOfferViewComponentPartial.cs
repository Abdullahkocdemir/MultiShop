using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.SpecialOfferService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _SpecialOfferViewComponentPartial : ViewComponent
    {
        private readonly ISpecialOfferService _specialOfferService;
        public _SpecialOfferViewComponentPartial(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }
    }
}