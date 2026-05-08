using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.FeatureSliderService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _CarouselDefaultViewComponentPartial : ViewComponent
    {
        private readonly IFeatureSliderService _featureSliderService;

        public _CarouselDefaultViewComponentPartial(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }
    }
}
