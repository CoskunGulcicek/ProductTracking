using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DataAccess.Concrete.Inferfaces
{
    public interface IGenericDal<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> keySelector);
        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
