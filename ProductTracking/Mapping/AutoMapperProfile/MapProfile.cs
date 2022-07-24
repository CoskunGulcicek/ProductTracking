using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Customer;
using Tracking.Entities.Dtos.CustomerProduct;
using Tracking.Entities.Dtos.List;
using Tracking.Entities.Dtos.ListCustomer;
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

            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductAddDto>();
            
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();
            
            CreateMap<ProductGetDto, Product>();
            CreateMap<Product, ProductGetDto>();

            CreateMap<CustomerProductAddDto, CustomerProduct>();
            CreateMap<CustomerProduct, CustomerProductAddDto>();

            CreateMap<CustomerProductGetDto, CustomerProduct>();
            CreateMap<CustomerProduct, CustomerProductGetDto>();

            CreateMap<ListAddDto, List>();
            CreateMap<List, ListAddDto>();

            CreateMap<ListUpdateDto, List>();
            CreateMap<List, ListUpdateDto>();

            CreateMap<ListGetDto, List>();
            CreateMap<List, ListGetDto>();

            CreateMap<ListCustomerAddDto, ListCustomer>();
            CreateMap<ListCustomer, ListCustomerAddDto>();

            CreateMap<ListCustomerGetDto, ListCustomer>();
            CreateMap<ListCustomer, ListCustomerGetDto>();

        }
    }
}
