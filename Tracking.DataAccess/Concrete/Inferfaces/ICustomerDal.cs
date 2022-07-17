using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Customer;

namespace Tracking.DataAccess.Concrete.Inferfaces
{
    public interface ICustomerDal : IGenericDal<Customer>
    {
        Task<List<CustomerGetDto>> GetByListId(int Id);
    }
}
