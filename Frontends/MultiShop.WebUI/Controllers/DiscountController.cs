using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.DiscountService;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        public IActionResult ConfirmCode(string code)
        {
            var couponCode = _discountService.GetDiscountCode(code);
            int x = 0;
            x = 5;
            return View(couponCode);
        }
    }
}
