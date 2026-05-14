using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketService;

namespace MultiShop.WebUI.ViewComponents.ShoppingCarComponentPartials
{
    public class _ShoppingCarProductListViewComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;

        public _ShoppingCarProductListViewComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _basketService.GetBasket();
            // Sepet boşsa boş bir liste gönderiyoruz ki View patlamasın
            var basketItems = values?.BasketItems ?? new List<MultiShop.DTOLayer.BasketDTOs.BasketItemDTO>();
            return View(basketItems);
        }
    }
}