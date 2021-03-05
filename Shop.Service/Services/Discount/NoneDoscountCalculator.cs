using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service.Services
{
    public class NoneDoscountCalculator : IDiscountCalculator
    {
        public decimal Calculate(IList<ProductBasketItem> productBasketItems)
        {
            return 0;
        }
    }
}
