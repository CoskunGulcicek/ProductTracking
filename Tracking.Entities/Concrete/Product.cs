using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Concrete
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Type { get; set; }// kg adet kasa bağ(bund)

        public List<BasketProduct> BasketProducts { get; set; }
    }
}
