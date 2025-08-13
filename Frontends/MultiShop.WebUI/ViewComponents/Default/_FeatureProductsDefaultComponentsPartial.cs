using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.Default
{
    public class _FeatureProductsDefaultComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
