using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.RepositoryContracts
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IBasketRepository BasketRepository { get; }
        public IBasketItemRepository BasketItemRepository { get; }
        Task<int> SaveChanges();
    }
}
