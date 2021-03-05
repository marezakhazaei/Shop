using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service.Services
{
    public class TableDiscountCalculator : IDiscountCalculator
    {
        private const decimal DiscountValue = 0.05m;

        public decimal Calculate(IList<ProductBasketItem> productBasketItems)
        {
            var discount = 0m;
            var tables = productBasketItems.Where(p => p.ProductCategory == ProductCategory.Table).ToList();
            if (tables.Count > 0)
            {
                var tablePrice = tables.FirstOrDefault().Price;
                var numberOfTables = tables.Sum(s => s.Count);
                discount = numberOfTables * tablePrice * DiscountValue;
            }
            return discount;
        }
    }
}
