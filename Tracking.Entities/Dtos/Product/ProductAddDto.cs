using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Dtos.Product
{
    public class ProductAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
