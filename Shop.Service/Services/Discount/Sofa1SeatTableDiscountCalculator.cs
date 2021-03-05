using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service.Services
{
    public class Sofa1SeatTableDiscountCalculator : IDiscountCalculator
    {
        private const decimal DiscountValue = 0.15m;

        public decimal Calculate(IList<ProductBasketItem> productBasketItems)
        {
            var discount = 0m;
            var tables = productBasketItems.Where(p => p.ProductCategory == ProductCategory.Table).ToList();
            var sofas = productBasketItems.Where(p => p.ProductCategory == ProductCategory.Sofa && p.NumberOfSeat == 1).ToList();
            if (tables.Count > 0 && sofas.Count > 0)
            {
                var tablePrice = tables.FirstOrDefault().Price;
                var sofaPrice = sofas.FirstOrDefault().Price;
                var numberOfTables = tables.Sum(s => s.Count);
                var numberOfSofas = sofas.Sum(s => s.Count);
                var bundle = (numberOfTables > numberOfSofas ? numberOfSofas : numberOfTables);
                discount = (tablePrice + sofaPrice) * bundle * DiscountValue;
            }
            return discount;
        }
    }
}
