
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;

namespace Tracking.DataAccess.Concrete.Inferfaces
{
    public interface ICustomerProductDal : IGenericDal<CustomerProduct>
    {
        //Task<List<CustomerProduct>> customerProducts();
    }
}
