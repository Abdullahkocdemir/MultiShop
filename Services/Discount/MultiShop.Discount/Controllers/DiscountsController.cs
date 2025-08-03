using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDiscountCouponList()
        {
            var values = await _discountService.GetAllCouponAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDiscountCoupon(int id)
        {
            var value = await _discountService.GetByIdCouponAsync(id);
            return Ok(value);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateCouponDTO dTO)
        {
            await _discountService.UpdateCouponAsync(dTO);
            return Ok("Kupon Başarılı Bir Şekilde Güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateCouponDTO dTO)
        {
            await _discountService.CreateCouponAsync(dTO);
            return Ok("Kupon Başarılı Bir Şekilde Oluşturuldu.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);
            return Ok("Kupon Başarılı Bir Şekilde Silindi");
        }
    }
}
