using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.Models;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment hostEnvironment)
        {
            this.productService = productService;
            this.hostEnvironment = hostEnvironment;
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            try
            {
                product.Photo = UploadedFile(product?.Image);
                await productService.Add(product);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        private string UploadedFile(IFormFile image)
        {
            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
