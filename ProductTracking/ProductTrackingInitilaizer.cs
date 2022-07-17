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
        public static async Task Seed(ICustomerService customerService, IProductService productService,IListService listService)
        {

            var lists = await listService.GetAllAsync();
            if (lists.Count < 1)
            {
                List<List> newList = new List<List>() {
                new List(){Name = "Köln"},
                new List(){Name = "Stutgart"},
                new List(){Name = "Munih"},
                new List(){Name = "Dortmund"}
                };
                foreach (var item in newList)
                {
                    await listService.AddAsync(item);
                }
            }

            var customers = await customerService.GetAllAsync();
            if (customers.Count<1)
            {
                List<Customer> customer = new List<Customer>() {
                new Customer(){Name = "Coşkun",SurName = "Gülçiçek",ListId=1},
                new Customer(){Name = "Kenan",SurName = "Kaplan",ListId=2},
                new Customer(){Name = "Uğur",SurName = "Tokgöz",ListId=3},
                new Customer(){Name = "Akın",SurName = "Kaçar",ListId=4},
                new Customer(){Name = "Aslı",SurName = "Seval",ListId=1},
                new Customer(){Name = "Kerem",SurName = "Zülfiye",ListId=2},
                new Customer(){Name = "Aykut",SurName = "Furkan",ListId=3},
                new Customer(){Name = "Ecrin",SurName = "Derya",ListId=4},
                new Customer(){Name = "Tufan",SurName = "Aslan",ListId=1},
                new Customer(){Name = "Ozan",SurName = "Zuhal",ListId=2},
                new Customer(){Name = "Merih",SurName = "Irmak",ListId=3},
                new Customer(){Name = "Aliye",SurName = "Derin",ListId=4},
                new Customer(){Name = "Kuddusi",SurName = "Cemil",ListId=1},
                new Customer(){Name = "Bilal",SurName = "Oktan",ListId=2},
                new Customer(){Name = "Görgün",SurName = "Kadir",ListId=3},
                new Customer(){Name = "Şahin",SurName = "Deli",ListId=4}
                };
                foreach (var item in customer)
                {
                    var addedCustomer = await customerService.AddAsync(item);
                }
            }

            var products = await productService.GetAllAsync();
            if (products.Count < 1)
            {
                List<Product> product = new List<Product>()
                {
                    new Product(){Name="Elma",Type="ks"},
                    new Product(){Name="Armut",Type="ks"},
                    new Product(){Name="Cola",Type="ks"},
                    new Product(){Name="Kalem",Type="ks"},
                    new Product(){Name="Defter",Type="ks"},
                    new Product(){Name="Silgi",Type="ks"},
                    new Product(){Name="Borcam",Type="ks"},
                    new Product(){Name="Zil",Type="ks"},
                    new Product(){Name="Pano",Type="ks"},
                    new Product(){Name="Terlik",Type="ks"},
                    new Product(){Name="Mont",Type="ks"},
                    new Product(){Name="Ceket",Type="ks"}
                };
                foreach (var item in product)
                {
                    await productService.AddAsync(item);
                }
            }
        }
    }
}
