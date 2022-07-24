using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTracking.Models
{
    public class CalculationModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public int cusId { get; set; }
        public int prodId { get; set; }
    }
}
