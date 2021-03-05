using Shop.Common.RepositoryContracts;
using Shop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext context;
        private readonly Lazy<ProductRepository> productRepository;
        private readonly Lazy<BasketRepository> basketRepository;
        private readonly Lazy<BasketItemRepository> basketItemRepository;

        public UnitOfWork(ShopDbContext context)
        {
            this.context = context;
            productRepository = new Lazy<ProductRepository>(() => new ProductRepository(context));
            basketRepository = new Lazy<BasketRepository>(() => new BasketRepository(context));
            basketItemRepository = new Lazy<BasketItemRepository>(() => new BasketItemRepository(context));
        }

        public IProductRepository ProductRepository => productRepository.Value;
        public IBasketRepository BasketRepository => basketRepository.Value;
        public IBasketItemRepository BasketItemRepository => basketItemRepository.Value;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }
    }
}
