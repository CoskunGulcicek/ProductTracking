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
    public class CustomerProductManager : GenericManager<CustomerProduct>, ICustomerProductService
    {
        private readonly IGenericDal<CustomerProduct> _genericDal;
        private readonly ICustomerProductDal _customerProductDal;

        public CustomerProductManager(IGenericDal<CustomerProduct> genericDal, ICustomerProductDal customerProductDal):base(genericDal)
        {
            _genericDal = genericDal;
            _customerProductDal = customerProductDal;
        }
    }
}
