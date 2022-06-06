using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Entities.Concrete
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }

        public string Type { get; set; } //kg adet kasa metre bağ(bund)
        public Decimal Quantity { get; set; }
        public Decimal Total { get; set; }

    }
}
