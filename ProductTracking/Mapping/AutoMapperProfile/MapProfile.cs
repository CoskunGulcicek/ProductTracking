using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Basket;
using Tracking.Entities.Dtos.Customer;
using Tracking.Entities.Dtos.Product;

namespace ProductTracking.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CustomerAddDto, Customer>();
            CreateMap<Customer, CustomerAddDto>();

            CreateMap<CustomerGetDto, Customer>();
            CreateMap<Customer, CustomerGetDto>();

            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerUpdateDto>();

            CreateMap<BasketAddDto, Basket>();
            CreateMap<Basket, BasketAddDto>();

            CreateMap<BasketGetDto, Basket>();
            CreateMap<Basket, BasketGetDto>();

            CreateMap<BasketUpdateDto, Basket>();
            CreateMap<Basket, BasketUpdateDto>();

            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductAddDto>();
            
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();
            
            CreateMap<ProductGetDto, Product>();
            CreateMap<Product, ProductGetDto>();



        }
    }
}
