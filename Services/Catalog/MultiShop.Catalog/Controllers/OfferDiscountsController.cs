using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.OfferDiscountDTO;
using MultiShop.Catalog.Services.OfferService;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferDiscountsController : ControllerBase
    {
        private readonly IOfferService _offerService;
        public OfferDiscountsController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<IActionResult> OfferDiscountList()
        {
            var values = await _offerService.GetAllOfferDiscountAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferDiscountById(string id)
        {
            var value = await _offerService.GetByIdOfferDiscountAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            await _offerService.CreateOfferDiscountAsync(createOfferDiscountDTO);
            return Ok("İndirim teklifi başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _offerService.DeleteOfferDiscountAsync(id);
            return Ok("İndirim teklifi başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            await _offerService.UpdateOfferDiscountAsync(updateOfferDiscountDTO);
            return Ok("İndirim teklifi başarıyla güncellendi");
        }
    }
}