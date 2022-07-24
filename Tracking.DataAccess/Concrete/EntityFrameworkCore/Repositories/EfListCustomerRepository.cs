using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.Inferfaces;
using Tracking.Entities.Concrete;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfListCustomerRepository : EfGenericRepository<ListCustomer>,IListCustomerDal
    {

    }
}
