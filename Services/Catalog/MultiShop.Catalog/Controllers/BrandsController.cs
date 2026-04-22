using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.BrandDTO;
using MultiShop.Catalog.Services.BrandService;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    //Test
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> BrandList()
        {
            var values = await _brandService.GetAllBrandAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(string id)
        {
            var value = await _brandService.GetByIdBrandAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrandDTO)
        {
            await _brandService.CreateBrandAsync(createBrandDTO);
            return Ok("Marka başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return Ok("Marka başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO)
        {
            await _brandService.UpdateBrandAsync(updateBrandDTO);
            return Ok("Marka başarıyla güncellendi");
        }
    }
}