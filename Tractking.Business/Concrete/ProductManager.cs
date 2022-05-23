using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.Inferfaces;
using Tracking.Entities.Concrete;
using Tractking.Business.Interfaces;

namespace Tractking.Business.Concrete
{
    public class ProductManager : GenericManager<Product> ,IProductService
    {
        private readonly IGenericDal<Product> _genericDal;
        private readonly IProductDal _productDal;

        public ProductManager(IGenericDal<Product> genericDal, IProductDal productDal):base(genericDal)
        {
            _genericDal = genericDal;
            _productDal = productDal;
        }
    }
}
