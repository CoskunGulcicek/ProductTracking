using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.EntityFrameworkCore.Context;
using Tracking.DataAccess.Concrete.Inferfaces;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Customer;
using Tracking.Entities.Dtos.Product;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCustomerRepository : EfGenericRepository<Customer>, ICustomerDal
    {

        public async Task<List<CustomerGetDto>> GetByListId(int Id)
        {
            using var Context = new TrackingContext();
            return await (Task<List<CustomerGetDto>>)Context.Customers.Where(x => x.ListId == Id).
                Select(x => new CustomerGetDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SurName = x.SurName,
                    Products = Context.CustomerProducts
                           .Where(cp => cp.CustomerId == x.Id)
                           .Select(c => new Entities.Concrete.CustomerProduct()
                           {
                               CustomerId = c.CustomerId,
                               Id = c.Id,
                               ProductId = c.ProductId,
                               ProductName = Context.Products
                                       .Where(p => p.Id == c.ProductId)
                                       .Select(c => c.Name)
                                       .FirstOrDefault()
                           }).ToList()
                }).ToListAsync();
        }

        public async Task<List<CustomerGetDto>> GetByLstCusIds(int ListId)
        {
            using var Context = new TrackingContext();
            var datas = await (from liscus in Context.ListCustomers
                         join lst in Context.Lists on liscus.ListId equals lst.Id
                         join cus in Context.Customers on liscus.CustomerId equals cus.Id
                         where liscus.ListId == ListId
                         select new CustomerGetDto
                         {
                             Id = cus.Id,
                             Name = cus.Name,
                             SurName = cus.SurName,
                             Products = Context.CustomerProducts
                                          .Where(cp => cp.CustomerId == cus.Id)
                                          .Select(c => new Entities.Concrete.CustomerProduct()
                                          {
                                              CustomerId = c.CustomerId,
                                              Id = c.Id,
                                              ProductId = c.ProductId,
                                              ProductName = Context.Products
                                                      .Where(p => p.Id == c.ProductId)
                                                      .Select(c => c.Name)
                                                      .FirstOrDefault(),
                                              Quantity = c.Quantity
                                          }).ToList()
                         }).ToListAsync();
            return datas;
        }
    }
}
