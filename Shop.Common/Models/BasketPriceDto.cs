using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class BasketPriceDto : BaseDto
    {
        public decimal TotalDiscount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
