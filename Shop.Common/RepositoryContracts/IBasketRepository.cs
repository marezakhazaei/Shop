using Shop.Common.Entities;
using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.RepositoryContracts
{
    public interface IBasketRepository : IRepository<Basket, long>
    {
        Task<Basket> GetCurrentUserBasket(string clientId);
        Task<Basket> GetDetailBasketItems(string clientId);
    }
}
