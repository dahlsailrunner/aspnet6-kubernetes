using AutoMapper;
using CarvedRock.Api.Data;
using CarvedRock.Shared.Models;

namespace CarvedRock.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
