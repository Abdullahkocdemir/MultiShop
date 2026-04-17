using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BussinessLayer.Absract; 
using MultiShop.Cargo.DTOLayer.DTOs.CargoDetail;
using MultiShop.Cargo.EntityLayer.Entities;   

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailList()
        {
            var values = await _cargoDetailService.BGetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoDetailById(int id)
        {
            var value = await _cargoDetailService.BGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kargo detayı bulunamadı.");
            }
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoDetail(CreateCargoDetailDTO createCargoDetailDTO)
        {
            await _cargoDetailService.BAddAsync(new CargoDetail
            {
                SenderCustomer = createCargoDetailDTO.SenderCustomer,
                ReceiverCustomer = createCargoDetailDTO.ReceiverCustomer,
                Barcode = createCargoDetailDTO.Barcode,
                CargoCompanyId = createCargoDetailDTO.CargoCompanyId
            });
            return Ok("Kargo detayları başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDTO updateCargoDetailDTO)
        {
            // 1. Önce güncellenecek veriyi veritabanından buluyoruz
            var existingDetail = await _cargoDetailService.BGetByIdAsync(updateCargoDetailDTO.CargoDetailId);

            if (existingDetail == null)
            {
                return NotFound("Güncellenecek kargo detayı bulunamadı.");
            }

            // 2. DTO'dan gelen güncel bilgileri mevcut Entity'e aktarıyoruz
            existingDetail.SenderCustomer = updateCargoDetailDTO.SenderCustomer;
            existingDetail.ReceiverCustomer = updateCargoDetailDTO.ReceiverCustomer;
            existingDetail.Barcode = updateCargoDetailDTO.Barcode;
            existingDetail.CargoCompanyId = updateCargoDetailDTO.CargoCompanyId;

            // 3. Servis katmanı üzerinden veritabanını güncelliyoruz
            await _cargoDetailService.BUpdateAsync(existingDetail);

            return Ok("Kargo detayı başarıyla güncellendi.");
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDTO updateCargoDetailDTO)
        //{
        //    var existingDetail = await _cargoDetailService.BGetByIdAsync(updateCargoDetailDTO.CargoDetailId);
        //    if (existingDetail == null)
        //    {
        //        return NotFound("Kargo detayı bulunamadı.");
        //    }

        //    existingDetail.SenderCustomer = updateCargoDetailDTO.SenderCustomer;
        //    existingDetail.ReceiverCustomer = updateCargoDetailDTO.ReceiverCustomer;
        //    existingDetail.Barcode = updateCargoDetailDTO.Barcode;
        //    existingDetail.CargoCompanyId = updateCargoDetailDTO.CargoCompanyId;

        //    await _cargoDetailService.BUpdateAsync(existingDetail);
        //    return Ok("Kargo detayları başarıyla güncellendi.");
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoDetail(int id)
        {
            await _cargoDetailService.BDeleteAsync(id);
            return Ok("Kargo detayı başarıyla silindi.");
        }
    }
}