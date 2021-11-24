using AutoMapper;
using FashionShop.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Models.Map
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            //CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<Color, CreateColorDto>().ReverseMap();
           // CreateMap<Color, UpdateColorDto>().ReverseMap();

            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Gender, CreateGenderDto>().ReverseMap();
            CreateMap<Gender, UpdateGenderDto>().ReverseMap();


            CreateMap<Material, MaterialDto>().ReverseMap();
            CreateMap<Material, CreateMaterialDto>().ReverseMap();
           // CreateMap<Material, UpdateMaterialDto>().ReverseMap();

            CreateMap<MaterialCategory, MaterialCategoryDto>().ReverseMap();
            CreateMap<MaterialCategory, CreateMaterialCategoryDto>().ReverseMap();
           // CreateMap<MaterialCategory, UpdateMaterialCategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
           // CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<ProductSize, ProductSizeDto>().ReverseMap();
            CreateMap<ProductSize, CreateProductSizeDto>().ReverseMap();
            //CreateMap<ProductSize, UpdateProductSizeDto>().ReverseMap();

            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<Size, CreateSizeDto>().ReverseMap();
           // CreateMap<Size, UpdateSizeDto>().ReverseMap();

            CreateMap<Style, StyleDto>().ReverseMap();
            CreateMap<Style, CreateStyleDto>().ReverseMap();
            // CreateMap<Style, UpdateStyleDto>().ReverseMap();

            CreateMap<ApiUser, UserDTO>().ReverseMap();

        }
    }
}
