using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;

namespace ProductTracking.Models
{
    public class CustomerProductModel
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
    }
}
