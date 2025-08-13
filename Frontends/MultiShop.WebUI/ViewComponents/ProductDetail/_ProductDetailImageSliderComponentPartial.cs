using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductDetail
{
    //[ViewComponent(Name = "ProductDetailImageSlider")]
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
