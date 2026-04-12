using AutoMapper;
using MultiShop.Catalog.DTOs.CategoryDTO;
using MultiShop.Catalog.DTOs.ProductDetailDTO;
using MultiShop.Catalog.DTOs.ProductDTO;
using MultiShop.Catalog.DTOs.ProductImageDTO;
using MultiShop.Catalog.Entities;
using System.Data;

namespace MultiShop.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //Kategori Maplemesi
            CreateMap<ResultCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();
            CreateMap<GetByIdCategoryDTO, Category>().ReverseMap();

            //Product Mapplemesi
            CreateMap<ResultProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<GetByIdProductDTO, Product>().ReverseMap();

            //ProductDetail Maplemesi

            CreateMap<ResultProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<UpdateProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<CreateProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<GetByIdProductImageDTO, ProductDetail>().ReverseMap();

            //ProductImage Maplemesi

            CreateMap<ResultProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<CreateProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<GetByIdProductImageDTO, ProductImage>().ReverseMap();


        }
    }
}
