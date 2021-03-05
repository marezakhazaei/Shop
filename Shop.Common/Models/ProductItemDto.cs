using Shop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class ProductItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductDesc { get; set; }
        public string Photo { get; set; }
        //public ProductCategory ProductCategory { get; set; }
    }
}
