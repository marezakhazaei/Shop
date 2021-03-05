using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service.Services
{
    public class Sofa2SeatDiscountCalculator : IDiscountCalculator
    {
        private const decimal DiscountValue = 0.15m;

        public decimal Calculate(IList<ProductBasketItem> productBasketItems)
        {
            var discount = 0m;
            var sofas = productBasketItems.Where(p => p.ProductCategory == ProductCategory.Sofa && p.NumberOfSeat == 2).ToList();
            if (sofas.Count > 0)
            {
                var sofaPrice = sofas.FirstOrDefault().Price;
                var numberOfSofas = sofas.Sum(s => s.Count);
                discount = numberOfSofas * sofaPrice * DiscountValue;
            }
            return discount;
        }
    }
}
