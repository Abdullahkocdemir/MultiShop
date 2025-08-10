using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCargoCompanies()
        {
            var cargoCompanies = _cargoCompanyService.BGetListAll();
            var cargoCompanyDtos = _mapper.Map<List<ResultCargoCompanyDTO>>(cargoCompanies);
            return Ok(cargoCompanyDtos);

        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyById(int id)
        {
            var cargoCompany = _cargoCompanyService.BGetById(id);
            if (cargoCompany == null)
                return NotFound($"ID: {id} olan kargo şirketi bulunamadı.");
            var cargoCompanyDto = _mapper.Map<GetByIdCargoCompanyDTO>(cargoCompany);
            return Ok(cargoCompanyDto);
        }

    

       
        

        [HttpPost]
        public ActionResult CreateCargoCompany(CreateCargoCompanyDTO createCargoCompanyDto)
        {

            var cargoCompany = _mapper.Map<CargoCompany>(createCargoCompanyDto);
            _cargoCompanyService.BAdd(cargoCompany);

            var createdCargoCompanyDto = _mapper.Map<GetByIdCargoCompanyDTO>(cargoCompany);

            return Ok("Kargo Şirketi Başarılı Bir Şekilde Oluşturuldu");


        }

        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDTO dto)
        {
            var existingModel = _cargoCompanyService.BGetById(dto.Id);
            if (existingModel == null)
            {
                return NotFound($"Model with ID {dto.Id} bulunamadı.");
            }
            _mapper.Map(dto, existingModel);
            _cargoCompanyService.BUpdate(existingModel);
            return Ok("Kargo şirketi başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoCompany(int id)
        {
            if (id <= 0)
                return BadRequest("Geçersiz ID değeri.");

            var cargoCompany = _cargoCompanyService.BGetById(id);
            _cargoCompanyService.BDelete(id);
            return Ok("Kargo Şirketi Başarılı Bir Şekilde Silindi");

        }
    }
}