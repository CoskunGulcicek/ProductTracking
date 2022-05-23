using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Dtos.Customer
{
    public class CustomerGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
