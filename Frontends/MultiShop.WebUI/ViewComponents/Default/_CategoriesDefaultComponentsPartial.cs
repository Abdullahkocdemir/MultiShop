using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.Default
{
    public class _CategoriesDefaultComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
