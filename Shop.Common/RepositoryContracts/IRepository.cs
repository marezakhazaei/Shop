using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.RepositoryContracts
{
    public interface IRepository<TEntity, TKey>
    {
        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task Delete(TKey id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate);
    }
}
