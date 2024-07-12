using AutoMapper;
using MwTesting.Dtos;
using MwTesting.Model;

namespace MwTesting.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

        }
    }
}