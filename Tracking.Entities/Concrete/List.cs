﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Concrete
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Default { get; set; }
        public List<Customer> Customers { get; set; }
        public List<ListCustomer> ListCustomers { get; set; }
    }
}
