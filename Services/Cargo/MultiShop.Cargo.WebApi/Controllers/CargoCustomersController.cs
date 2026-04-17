using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoCustomer;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = await _cargoCustomerService.BGetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCustomerById(int id)
        {
            var value = await _cargoCustomerService.BGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Müşteri bulunamadı.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDTO createCargoCustomerDTO)
        {
            await _cargoCustomerService.BAddAsync(new CargoCustomer
            {
                Name = createCargoCustomerDTO.Name,
                SurName = createCargoCustomerDTO.SurName,
                Email = createCargoCustomerDTO.Email,
                Phone = createCargoCustomerDTO.Phone,
                District = createCargoCustomerDTO.District,
                City = createCargoCustomerDTO.City,
                Address = createCargoCustomerDTO.Address
            });
            return Ok("Müşteri başarıyla oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDTO updateCargoCustomerDTO)
        {
            var existingCustomer = await _cargoCustomerService.BGetByIdAsync(updateCargoCustomerDTO.CargoCustomerId);
            if (existingCustomer == null)
            {
                return NotFound("Güncellenecek müşteri bulunamadı.");
            }

            existingCustomer.Name = updateCargoCustomerDTO.Name;
            existingCustomer.SurName = updateCargoCustomerDTO.SurName;
            existingCustomer.Email = updateCargoCustomerDTO.Email;
            existingCustomer.Phone = updateCargoCustomerDTO.Phone;
            existingCustomer.District = updateCargoCustomerDTO.District;
            existingCustomer.City = updateCargoCustomerDTO.City;
            existingCustomer.Address = updateCargoCustomerDTO.Address;

            await _cargoCustomerService.BUpdateAsync(existingCustomer);
            return Ok("Müşteri bilgileri başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoCustomer(int id)
        {
            await _cargoCustomerService.BDeleteAsync(id);
            return Ok("Müşteri başarıyla silindi.");
        }
    }
}