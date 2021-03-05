using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service.Services
{
    public class StaffDiscountCalculator : IDiscountCalculator
    {
        private const decimal DiscountValue = 0.10m;

        public decimal Calculate(IList<ProductBasketItem> productBasketItems)
        {
            var discount = 0m;
            if (productBasketItems.Count > 0)
            {
                var ls = (from item in productBasketItems
                          group item by item.Price into g
                          select new { Price = g.Key, Total = g.Sum(d => d.Count) }).ToList();
                var totalPrice = ls.Sum(s => s.Total * s.Price);
                discount = totalPrice * DiscountValue;
            }
            return discount;
        }
    }
}
