using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;

namespace ProductTracking.Models
{
    public class ListAndCustomerModel
    {
        public List<List> lists { get; set; }
        public List<Customer> customers { get; set; }
    }
}
