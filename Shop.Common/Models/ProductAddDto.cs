using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class ProductAddDto
    {
        public string ClientId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
    }
}
