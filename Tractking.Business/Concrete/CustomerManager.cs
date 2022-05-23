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
    public class CustomerManager : GenericManager<Customer> ,ICustomerService
    {
        private readonly IGenericDal<Customer> _genericDal;
        private readonly ICustomerDal _customerDal;
        public CustomerManager(IGenericDal<Customer> genericDal, ICustomerDal customerDal):base(genericDal)
        {
            _genericDal = genericDal;
            _customerDal = customerDal;
        }
    }
}
