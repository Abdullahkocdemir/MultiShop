using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
