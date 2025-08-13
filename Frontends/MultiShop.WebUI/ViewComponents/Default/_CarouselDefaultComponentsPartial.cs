using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.Default
{
    public class _CarouselDefaultComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
