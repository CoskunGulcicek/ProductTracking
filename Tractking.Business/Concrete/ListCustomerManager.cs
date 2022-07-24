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
    public class ListCustomerManager : GenericManager<ListCustomer>, IListCustomerService
    {
        private readonly IGenericDal<ListCustomer> _genericDal;
        private readonly IListCustomerDal _listCustomerDal;

        public ListCustomerManager(IGenericDal<ListCustomer> genericDal, IListCustomerDal listCustomerDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _listCustomerDal = listCustomerDal;
        }
    }
}
