using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.Default
{
    public class _FooterDefaultLayoutComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
