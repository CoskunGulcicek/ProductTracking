using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tractking.Business.Interfaces;

namespace ProductTracking
{
    public static class ProductTrackingInitilaizer
    {
        public static async Task Seed(ICustomerService customerService, IProductService productService, IBasketService basketService, IBasketProductService basketProductService)
        {
            var customers = await customerService.GetAllAsync();
            if (customers.Count<1)
            {
                List<Customer> customer = new List<Customer>() {
                new Customer(){Name = "Coşkun",SurName = "Gülçiçek"},
                new Customer(){Name = "Kenan",SurName = "Kaplan"},
                new Customer(){Name = "Uğur",SurName = "Tokgöz"},
                new Customer(){Name = "Akın",SurName = "Kaçar"}
                };
                foreach (var item in customer)
                {
                    var addedCustomer = await customerService.AddAsync(item);
                    Basket basket = new Basket();
                    basket.CustomerId = addedCustomer.Id;
                    await basketService.AddAsync(basket);
                }
            }

            var products = await productService.GetAllAsync();
            if (products.Count < 1)
            {
                List<Product> product = new List<Product>()
                {
                    new Product(){Name="Elma",Price=5,Type="kg"},
                    new Product(){Name="Armut",Price=6,Type="kg"},
                    new Product(){Name="Cola",Price=3,Type="lt"},
                    new Product(){Name="Kalem",Price=10,Type="co"}
                };
                foreach (var item in product)
                {
                    await productService.AddAsync(item);
                }
            }
        }
    }
}
