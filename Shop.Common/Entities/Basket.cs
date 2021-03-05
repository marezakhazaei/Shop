using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Entities
{
    public class Basket : BaseEntity<long>
    {
        public Basket()
        {
            BasketItems = new HashSet<BasketItem>();
        }

        public decimal TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal ProductPrices { get; set; }
        public string ClientId { get; set; }
        public short BasketStatus { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
