using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.BasketDTOs;
using MultiShop.WebUI.Services.BasketService;
using MultiShop.WebUI.Services.CatalogService.ProductService;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCarController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShoppingCarController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket();
            return View(basket);
        }
        //[HttpPost]
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var product = await _productService.GetByIdProductAsync(id);
            var Item = new BasketItemDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.ProductPrice,
                Quantity = 1,
                ImageURL= product.ProductImageUrl
            };
            await _basketService.AddBasketItem(Item);
            return RedirectToAction("Index");
        }

        [HttpGet] // Linkler GET isteği gönderir
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }

    }
}
