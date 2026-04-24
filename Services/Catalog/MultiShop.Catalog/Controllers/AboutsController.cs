using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.AboutDTO;
using MultiShop.Catalog.Services.AboutService;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous] // Geliştirme aşamasında erişim kolaylığı için, istersen kaldırabilirsin
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await _aboutService.GetAllAboutAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            var value = await _aboutService.GetByIdIAboutAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            await _aboutService.CreateAboutAsync(createAboutDTO);
            return Ok("Hakkımızda alanı başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return Ok("Hakkımızda alanı başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDTO);
            return Ok("Hakkımızda alanı başarıyla güncellendi");
        }
    }
}