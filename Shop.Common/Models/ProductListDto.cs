using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class ProductListDto : BaseDto
    {
        public int TotalRows { get; set; }
        public IEnumerable<ProductItemDto> Products { get; set; }
    }
}
