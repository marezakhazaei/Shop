using Microsoft.EntityFrameworkCore;
using Shop.Common.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories
{

    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        private readonly ShopDbContext context;

        public Repository(ShopDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var result = await context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public virtual async Task Delete(TKey id)
        {
            var find = await Get(id);
            if (find != null)
            {
                Delete(find);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> Get(TKey id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
    }
}
