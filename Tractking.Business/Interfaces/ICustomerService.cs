using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;
using Tracking.Entities.Dtos.Customer;

namespace Tractking.Business.Interfaces
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<List<CustomerGetDto>> GetByListId(int Id);
        Task<List<CustomerGetDto>> GetByLstCusIds(int ListId);

    }
}
