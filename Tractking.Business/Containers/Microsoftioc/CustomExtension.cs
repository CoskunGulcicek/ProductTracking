using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using Tracking.DataAccess.Concrete.Inferfaces;
using Tractking.Business.Concrete;
using Tractking.Business.Interfaces;

namespace Tractking.Business.Containers.Microsoftioc
{
    public static class CustomExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<ICustomerDal, EfCustomerRepository>();
            services.AddScoped<ICustomerService, CustomerManager>();

            services.AddScoped<IProductDal, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IBasketDal, EfBasketRepository>();
            services.AddScoped<IBasketService, BasketManager>();
        }
    }
}
