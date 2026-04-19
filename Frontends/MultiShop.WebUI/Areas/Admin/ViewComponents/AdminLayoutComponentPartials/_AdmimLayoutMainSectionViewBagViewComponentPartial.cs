using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutComponentPartials
{
    public class _AdmimLayoutMainSectionViewBagViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //ViewBag.v1 = "Ana SAyfa";
            //ViewBag.v2 = "Kategoriler";
            //ViewBag.v3 = "Kategori Listesi";
            //ViewBag.Title = "Kategori İşlemleri";
            return View();
        }
    }
}
