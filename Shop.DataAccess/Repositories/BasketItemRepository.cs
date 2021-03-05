using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;
using Shop.Common.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories
{
    public class BasketItemRepository : Repository<BasketItem, long>, IBasketItemRepository
    {
        private readonly ShopDbContext dbContext;

        public BasketItemRepository(ShopDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetBasketItemCount(long basketId)
        {
            return await dbContext.BasketItems.CountAsync(p => p.BasketId == basketId);
        }

        public async Task<int> GetBasketItemCount(string clientId)
        {
            return await dbContext.BasketItems.Include(p => p.Basket).Where(d => d.Basket.ClientId == clientId).SumAsync(s => s.ProductCount);
        }
    }
}
