using AutoMapper;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.DtoLayer.CargoDetailDtos;
using MultiShop.Cargo.DtoLayer.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CargoCompany, GetByIdCargoCompanyDTO>().ReverseMap();
            CreateMap<CargoCompany, UpdateCargoCompanyDTO>().ReverseMap();
            CreateMap<CargoCompany, CreateCargoCompanyDTO>().ReverseMap();
            CreateMap<CargoCompany, ResultCargoCompanyDTO>().ReverseMap();

            CreateMap<CargoDetail, GetByIdCargoDetailDTO>().ReverseMap();
            CreateMap<CargoDetail, UpdateCargoDetailDTO>().ReverseMap();
            CreateMap<CargoDetail, CreateCargoDetailDTO>().ReverseMap();
            CreateMap<CargoDetail, ResultCargoDetailDTO>().ReverseMap();



            CreateMap<CargoCustomer, GetByIdCargoCustomerDTO>().ReverseMap();
            CreateMap<CargoCustomer, UpdateCargoCustomerDTO>().ReverseMap();
            CreateMap<CargoCustomer, CreateCargoCustomerDTO>().ReverseMap();
            CreateMap<CargoCustomer, ResultCargoCustomerDTO>().ReverseMap();


            CreateMap<CargoOperation, GetByIdCargoOperationDTO>().ReverseMap();
            CreateMap<CargoOperation, UpdateCargoOperationDTO>().ReverseMap();
            CreateMap<CargoOperation, CreateCargoOperationDTO>().ReverseMap();
            CreateMap<CargoOperation, ResultCargoOperationDTO>().ReverseMap();
        }

    }
}
