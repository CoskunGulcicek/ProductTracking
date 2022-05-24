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
    public class BasketManager : GenericManager<Basket>, IBasketService
    {
        private readonly IGenericDal<Basket> _genericDal;
        private readonly IBasketDal _basketDal;
        public BasketManager(IGenericDal<Basket> genericDal, IBasketDal basketDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _basketDal = basketDal;
        }
    }
}
