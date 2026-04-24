using AutoMapper;
using MultiShop.Catalog.DTOs.AboutDTO;
using MultiShop.Catalog.DTOs.BrandDTO;
using MultiShop.Catalog.DTOs.CategoryDTO;
using MultiShop.Catalog.DTOs.FeatureDTO;
using MultiShop.Catalog.DTOs.FeatureSliderDTO;
using MultiShop.Catalog.DTOs.OfferDiscountDTO;
using MultiShop.Catalog.DTOs.ProductDetailDTO;
using MultiShop.Catalog.DTOs.ProductDTO;
using MultiShop.Catalog.DTOs.ProductImageDTO;
using MultiShop.Catalog.DTOs.SpecialOfferDTO;
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
            CreateMap<Product, ResultProductWithCategoryDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<ResultProductWithCategoryDTO, Product>();

            //ProductDetail Maplemesi

            CreateMap<ResultProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<UpdateProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<CreateProductDetailDTO, ProductDetail>().ReverseMap();
            CreateMap<GetByIdProductDetailDTO, ProductDetail>().ReverseMap();

            //ProductImage Maplemesi

            CreateMap<ResultProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<CreateProductImageDTO, ProductImage>().ReverseMap();
            CreateMap<GetByIdProductImageDTO, ProductImage>().ReverseMap();

            //FeatureSlider Maplemesi
            CreateMap<FeatureSlider, ResultFeatureSliderDTO>().ReverseMap();
            CreateMap<FeatureSlider, UpdateFeatureSliderDTO>().ReverseMap();
            CreateMap<FeatureSlider, CreateFeatureSliderDTO>().ReverseMap();
            CreateMap<FeatureSlider, GetByIdFeatureSliderDTO>().ReverseMap();

            //SpecialOffer Meplemesi
            CreateMap<SpecialOffer, ResultSpecialOfferDTO>().ReverseMap();
            CreateMap<SpecialOffer, UpdateSpecialOfferDTO>().ReverseMap();
            CreateMap<SpecialOffer, CreateSpecialOfferDTO>().ReverseMap();
            CreateMap<SpecialOffer, GetByIdSpecialOfferDTO>().ReverseMap();

            //Feature Maplemesi

            CreateMap<Feature, ResultFeatureDTO>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDTO>().ReverseMap();
            CreateMap<Feature, CreateFeatureDTO>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDTO>().ReverseMap();

            //Brannd Maplemesi
            CreateMap<Brand, ResultBrandDTO>().ReverseMap();
            CreateMap<Brand, UpdateBrandDTO>().ReverseMap();
            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
            CreateMap<Brand, GetByIdBrandDTO>().ReverseMap();

            //Offer Discount Maplemesi

            CreateMap<OfferDiscount, ResultOfferDiscountDTO>().ReverseMap();
            CreateMap<OfferDiscount, UpdateOfferDiscountDTO>().ReverseMap();
            CreateMap<OfferDiscount, CreateOfferDiscountDTO>().ReverseMap();
            CreateMap<OfferDiscount, GetByIdOfferDiscountDTO>().ReverseMap();

            //About Maplemesi
            CreateMap<About, ResultAboutDTO>().ReverseMap();
            CreateMap<About, UpdateAboutDTO>().ReverseMap();
            CreateMap<About, CreateAboutDTO>().ReverseMap();
            CreateMap<About, GetByIdAboutDTO>().ReverseMap();


        }
    }
}
