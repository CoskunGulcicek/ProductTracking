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
    public class ListManager : GenericManager<Tracking.Entities.Concrete.List>, IListService
    {
        private readonly IGenericDal<Tracking.Entities.Concrete.List> _genericDal;
        private readonly IListDal _listDal;

        public ListManager(IGenericDal<List> genericDal, IListDal listDal):base(genericDal)
        {
            _genericDal = genericDal;
            _listDal = listDal;
        }
    }
}
