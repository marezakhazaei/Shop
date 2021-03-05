using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Entities
{
    public class BasketItem: BaseEntity<long>
    {
        public long BasketId { get; set; }
        public long ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal Price { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
