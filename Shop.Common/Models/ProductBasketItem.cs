using Shop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class ProductBasketItem
    {
        public ProductCategory ProductCategory { get; set; }
        public short? NumberOfSeat { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
