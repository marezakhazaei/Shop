using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Service.Services
{
    public class DiscountFactory
    {
        public static IDiscountCalculator GetCalculator(DiscountType discountType)
        {
            IDiscountCalculator calculator = discountType switch
            {
                DiscountType.None => new NoneDoscountCalculator(),
                DiscountType.Staff => new StaffDiscountCalculator(),
                DiscountType.Table => new TableDiscountCalculator(),
                DiscountType.Safa2Seat => new Sofa2SeatDiscountCalculator(),
                DiscountType.Sofa1Seat_Table => new Sofa1SeatTableDiscountCalculator(),
                _ => new NoneDoscountCalculator(),
            };
            return calculator;
        }

        public static decimal GetDicsount(IList<ProductBasketItem> productBasketItems, bool isStaff)
        {
            List<decimal> discounts = new List<decimal>();
            if (isStaff)
            {
                discounts.Add(GetCalculator(DiscountType.Staff).Calculate(productBasketItems));
            }

            discounts.Add(GetCalculator(DiscountType.Table).Calculate(productBasketItems));
            discounts.Add(GetCalculator(DiscountType.Sofa1Seat_Table).Calculate(productBasketItems));
            discounts.Add(GetCalculator(DiscountType.Safa2Seat).Calculate(productBasketItems));

            return discounts.Max();
        }
    }
}
