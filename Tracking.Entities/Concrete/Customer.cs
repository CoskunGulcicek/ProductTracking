using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Concrete
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public int? ListId { get; set; }
        public List List { get; set; }

        public List<CustomerProduct> CustomerProducts { get; set; }

    }
}
