using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IMapper _mapper;

        public CargoDetailsController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCargoDetail()
        {
            var cargoCompanies = _cargoDetailService.BGetListAll();
            var cargoCompanyDtos = _mapper.Map<List<ResultCargoDetailDTO>>(cargoCompanies);
            return Ok(cargoCompanyDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoDetailById(int id)
        {
            var cargoCompany = _cargoDetailService.BGetById(id);
            if (cargoCompany == null)
                return NotFound($"ID: {id} olan kargo detayı bulunamadı.");
            var cargoCompanyDto = _mapper.Map<GetByIdCargoDetailDTO>(cargoCompany);
            return Ok(cargoCompanyDto);
        }

        [HttpPost]
        public ActionResult CreateCargoDetail(CreateCargoDetailDTO createCargoCompanyDto)
        {
            var cargoDetail = _mapper.Map<CargoDetail>(createCargoCompanyDto);
            _cargoDetailService.BAdd(cargoDetail);
            var createdCargoCompanyDto = _mapper.Map<GetByIdCargoDetailDTO>(cargoDetail);
            return Ok("Kargo Detayı Başarılı Bir Şekilde Oluşturuldu");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDTO dto)
        {
            var existingModel = _cargoDetailService.BGetById(dto.Id);
            if (existingModel == null)
            {
                return NotFound($"Model with ID {dto.Id} bulunamadı.");
            }
            _mapper.Map(dto, existingModel);
            _cargoDetailService.BUpdate(existingModel);
            return Ok("Kargo Detayı başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoDetail(int id)
        {
            _cargoDetailService.BDelete(id);
            return Ok("Kargo Detayı Başarılı Bir Şekilde Silindi");
        }
    }
}
