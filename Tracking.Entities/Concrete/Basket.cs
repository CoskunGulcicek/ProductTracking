using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Concrete
{
    public class Basket
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }

    }
}
