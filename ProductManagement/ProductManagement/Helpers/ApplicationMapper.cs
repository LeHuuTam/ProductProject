using AutoMapper;
using ProductManagement.Commands;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Helpers
{
    public class ApplicationMapper : Profile
    {   
        public ApplicationMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductModel>().ReverseMap();
        }
    }
}
