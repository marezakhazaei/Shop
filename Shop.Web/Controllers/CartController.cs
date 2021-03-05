using Microsoft.AspNetCore.Mvc;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IBasketService basketService;

        public CartController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [Route("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var result = await basketService.Get(id);
            ViewData["BasketTotalCount"] = result.TotalItem;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductAddDto product)
        {
            var response = await basketService.Add(product);
            return Ok(response);
        }

        [Route("{clientId}/delete/{id}")]
        public async Task<IActionResult> Delete(string clientId, long id)
        {
            var result = await basketService.Delete(id);
            return RedirectToAction("Index", "Cart", new { id = clientId });
        }

        [Route("count/{id}")]
        public async Task<IActionResult> Count(string id)
        {
            var response = await basketService.CountBasketItems(id);
            return Ok(response);
        }

        [HttpPost("discount")]
        public async Task<IActionResult> Discount([FromBody] DiscountDto discountDto)
        {
            var response = await basketService.CaculateDiscount(discountDto.ClientId, discountDto.IsStaff);
            return Ok(response);
        }
    }
}
