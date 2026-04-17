using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoCompany;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await _cargoCompanyService.BGetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCompanyById(int id)
        {
            var value = await _cargoCompanyService.BGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kargo firması bulunamadı.");
            }
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDTO cargoCompany)
        {
            await _cargoCompanyService.BAddAsync(new EntityLayer.Entities.CargoCompany
            {
                CargoCompanyName = cargoCompany.CargoCompanyName
            });
            return Ok("Kargo şirketi başarıyla oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDTO cargoCompany)
        {
            var existingCompany = await _cargoCompanyService.BGetByIdAsync(cargoCompany.CargoCompanyId);
            if (existingCompany == null)
            {
                return NotFound("Kargo firması bulunamadı.");
            }
            existingCompany.CargoCompanyName = cargoCompany.CargoCompanyName;
            await _cargoCompanyService.BUpdateAsync(existingCompany);
            return Ok("Kargo şirketi başarıyla güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCompany(int id)
        {
            await _cargoCompanyService.BDeleteAsync(id);
            return Ok("Kargo firması başarılı bir şekilde silindi.!");
        }
    }
}