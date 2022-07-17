using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.Inferfaces;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfListRepository : EfGenericRepository<Tracking.Entities.Concrete.List>, IListDal
    {
    }
}
