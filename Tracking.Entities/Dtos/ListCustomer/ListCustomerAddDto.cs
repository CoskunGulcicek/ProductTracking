using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Dtos.ListCustomer
{
    public class ListCustomerAddDto
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public int CustomerId { get; set; }

    }
}
