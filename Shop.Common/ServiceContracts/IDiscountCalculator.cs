using Shop.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.ServiceContracts
{
    public interface IDiscountCalculator
    {
        public decimal Calculate(IList<ProductBasketItem> productBasketItems);
    }
}
