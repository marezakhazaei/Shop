using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class BasketItemDto
    {
        public long Id { get; set; }
        public long Basketid { get; set; }
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductCount { get; set; }
        public decimal Price { get; set; }
    }
}
