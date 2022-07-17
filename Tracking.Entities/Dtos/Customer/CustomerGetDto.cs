using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Entities.Dtos.Product;

namespace Tracking.Entities.Dtos.Customer
{
    public class CustomerGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public List<Entities.Concrete.CustomerProduct> Products { get; set; }
    }
}
