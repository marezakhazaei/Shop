using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Common.Models
{
    public class BasketDto : BaseDto
    {
        public BasketDto()
        {
            BasketItems = new List<BasketItemDto>();
        }
        public long BasketId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItem { get; set; }
        public decimal TotalDiscount { get; set; }
        public int TotalCount => BasketItems.Sum(o => o.ProductCount);
        public decimal ProductPrices { get; set; }
        public bool IsStaff { get; set; }
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
    }
}
