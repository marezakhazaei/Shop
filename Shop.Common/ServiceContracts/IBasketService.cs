using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.ServiceContracts
{
    public interface IBasketService
    {
        Task<BasketItemCountResultDto> Add(ProductAddDto product);
        Task<BasketDto> Get(string clientId);
        Task<EmptyResultDto> Delete(long basketItemId);
        Task<BasketItemCountResultDto> CountBasketItems(string clientId);
        Task<BasketPriceDto> CaculateDiscount(string clientId, bool isStaff);
    }
}
