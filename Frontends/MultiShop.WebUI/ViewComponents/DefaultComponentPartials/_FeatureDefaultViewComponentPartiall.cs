using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.FeatureService;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _FeatureDefaultViewComponentPartiall : ViewComponent
    {
        private readonly IFeatureService _featureService;
        public _FeatureDefaultViewComponentPartiall(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featureService.GetAllFeatureAsync();
            return View(values);

        }
    }
}