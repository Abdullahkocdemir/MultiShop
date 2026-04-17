using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoOperation;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoOperationList()
        {
            var values = await _cargoOperationService.BGetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoOperationById(int id)
        {
            var value = await _cargoOperationService.BGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kargo operasyon kaydı bulunamadı.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoOperation(CreateCargoOperationDTO createCargoOperationDTO)
        {
            await _cargoOperationService.BAddAsync(new CargoOperation
            {
                Barcode = createCargoOperationDTO.Barcode,
                Description = createCargoOperationDTO.Description,
                OperationDate = createCargoOperationDTO.OperationDate.DateTime // DateTimeOffset'ten DateTime'a dönüşüm
            });
            return Ok("Kargo operasyon süreci başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperation(UpdateCargoOperationDTO updateCargoOperationDTO)
        {
            var existingOperation = await _cargoOperationService.BGetByIdAsync(updateCargoOperationDTO.CargoOperationId);
            if (existingOperation == null)
            {
                return NotFound("Güncellenecek operasyon kaydı bulunamadı.");
            }

            existingOperation.Barcode = updateCargoOperationDTO.Barcode;
            existingOperation.Description = updateCargoOperationDTO.Description;
            existingOperation.OperationDate = updateCargoOperationDTO.OperationDate.DateTime;

            await _cargoOperationService.BUpdateAsync(existingOperation);
            return Ok("Kargo operasyon süreci başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoOperation(int id)
        {
            await _cargoOperationService.BDeleteAsync(id);
            return Ok("Kargo operasyon kaydı başarıyla silindi.");
        }
    }
}