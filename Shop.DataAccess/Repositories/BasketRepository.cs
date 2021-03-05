using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;
using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories
{
    public class BasketRepository : Repository<Basket, long>, IBasketRepository
    {
        private readonly ShopDbContext dbContext;

        public BasketRepository(ShopDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Basket> GetDetailBasketItems(string clientId)
        {
            var activeBasketStatus = (short)BasketStatus.Active;
            var res = await dbContext.Baskets.Include(i => i.BasketItems).ThenInclude(s => s.Product).Where(p => p.ClientId == clientId && p.BasketStatus == activeBasketStatus).FirstOrDefaultAsync();
            return res;
        }

        public async Task<Basket> GetCurrentUserBasket(string clientId)
        {
            var activeBasketStatus = (short)BasketStatus.Active;
            var result = await dbContext.Baskets.Where(p => p.ClientId == clientId && p.BasketStatus == activeBasketStatus).FirstOrDefaultAsync();
            return result;
        }

        
    }
}
