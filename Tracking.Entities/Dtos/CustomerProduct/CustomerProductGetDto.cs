using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Dtos.CustomerProduct
{
    public class CustomerProductGetDto
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }

    }
}
