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
    public class BasketProductManager : GenericManager<BasketProduct>, IBasketProductService
    {
        private readonly IGenericDal<BasketProduct> _genericDal;
        private readonly IBasketProductDal _basketProductDal;
        public BasketProductManager(IGenericDal<BasketProduct> genericDal, IBasketProductDal basketProductDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _basketProductDal = basketProductDal;
        }
    }
}
