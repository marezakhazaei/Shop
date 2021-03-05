using Shop.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.RepositoryContracts
{
    public interface IBasketItemRepository : IRepository<BasketItem, long>
    {
        Task<int> GetBasketItemCount(long basketId);
        Task<int> GetBasketItemCount(string clientId);
    }
}
