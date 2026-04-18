using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _FeatureDefaultViewComponentPartiall:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
