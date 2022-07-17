using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;

namespace ProductTracking.Models
{
    public class AddProductToCustomer
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
