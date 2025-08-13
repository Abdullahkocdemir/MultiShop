using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.Default
{
    public class _VendorDefaultLayoutComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
