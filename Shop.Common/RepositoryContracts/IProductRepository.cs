using Shop.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.RepositoryContracts
{
    public interface IProductRepository : IRepository<Product, long>
    {
        Task<IEnumerable<Product>> List(int page, int limit);
        Task<int> CountAll();
    }
}
