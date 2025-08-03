using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IMapper _mapper;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            _cargoCustomerService = cargoCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCargoCustomer()
        {
            var cargoCompanies = _cargoCustomerService.BGetListAll();
            var cargoCompanyDtos = _mapper.Map<List<ResultCargoCustomerDTO>>(cargoCompanies);
            return Ok(cargoCompanyDtos);

        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var cargoCompany = _cargoCustomerService.BGetById(id);
            if (cargoCompany == null)
                return NotFound($"ID: {id} olan kargo Kullanıcısı bulunamadı.");
            var cargoCompanyDto = _mapper.Map<GetByIdCargoCustomerDTO>(cargoCompany);
            return Ok(cargoCompanyDto);
        }

        [HttpPost]
        public ActionResult CreateCargoCustomer(CreateCargoCustomerDTO createCargoCustomerDTO)
        {

            var cargoCustomer = _mapper.Map<CargoCustomer>(createCargoCustomerDTO);
            _cargoCustomerService.BAdd(cargoCustomer);

            var createdCargoCompanyDto = _mapper.Map<GetByIdCargoCustomerDTO>(cargoCustomer);

            return Ok("Kargo Kullanıcısı Başarılı Bir Şekilde Oluşturuldu");


        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDTO dto)
        {
            var existingModel = _cargoCustomerService.BGetById(dto.Id);
            if (existingModel == null)
            {
                return NotFound($"Model with ID {dto.Id} bulunamadı.");
            }
            _mapper.Map(dto, existingModel);
            _cargoCustomerService.BUpdate(existingModel);
            return Ok("Kargo Kullanıcısı başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCargoCustomer(int id)
        {
            if (id <= 0)
                return BadRequest("Geçersiz ID değeri.");

            var cargoCompany = _cargoCustomerService.BGetById(id);
            _cargoCustomerService.BDelete(id);
            return Ok("Kargo Kullanıcısı Başarılı Bir Şekilde Silindi");

        }
    }
}
