using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginService;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IBasketService _basketService;

        public BasketsController(ILoginService loginService, IBasketService basketService)
        {
            _loginService = loginService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            // Kullanıcı ID'sini token üzerinden LoginService ile alıyoruz
            var userId = _loginService.GetUserId;
            var values = await _basketService.GetBasket(userId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            // Sepet kaydedilirken kullanıcı ID'sini garantiye alıyoruz
            basketTotalDTO.UserId = _loginService.GetUserId;
            await _basketService.SaveBasket(basketTotalDTO);
            return Ok("Sepet başarıyla kaydedildi veya güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            await _basketService.DeleteBasket(_loginService.GetUserId);
            return Ok("Sepet başarıyla silindi.");
        }
    }
}