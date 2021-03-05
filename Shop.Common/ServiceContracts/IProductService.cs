using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.ServiceContracts
{
    public interface IProductService
    {
        Task<ProductListDto> List(int page, int limit);
        Task<EmptyResultDto> Add(ProductDto productDto);
    }
}
