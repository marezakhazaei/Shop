using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;
using Shop.Common.RepositoryContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        private readonly ShopDbContext dbContext;

        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CountAll()
        {
            return await dbContext.Products.CountAsync();
        }

        public async Task<IEnumerable<Product>> List(int page, int limit)
        {
            return await dbContext.Products.AsNoTracking().Skip((page - 1) * limit).Take(limit).ToListAsync();
        }
    }
}
