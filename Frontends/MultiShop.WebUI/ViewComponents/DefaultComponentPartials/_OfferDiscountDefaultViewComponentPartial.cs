using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.OfferService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _OfferDiscountDefaultViewComponentPartial : ViewComponent
    {
        private readonly IOfferService _offerService;
        public _OfferDiscountDefaultViewComponentPartial(IOfferService offerService)
        {
            _offerService = offerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _offerService.GetAllOfferDiscountAsync();
            return View(values);
        }
    }
}
