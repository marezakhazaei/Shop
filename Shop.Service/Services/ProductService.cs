using Shop.Common.Entities;
using Shop.Common.Enums;
using Shop.Common.Models;
using Shop.Common.RepositoryContracts;
using Shop.Common.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ProductListDto> List(int page, int limit)
        {
            var response = new ProductListDto();
            var count = await unitOfWork.ProductRepository.CountAll();
            if (count != 0)
            {
                response.TotalRows = count;
                var products = await unitOfWork.ProductRepository.List(page, limit);
                if (products != null && products.Count() != 0)
                {
                    response.Products = products.Select(s => new ProductItemDto
                    {
                        Id = s.Id,
                        Photo = s.Photo,
                        Price = s.Price,
                        ProductDesc = s.ProductDesc,
                        Title = s.Title,
                    });
                }
            }
            else
            {
                response.ErrorType = ErrorType.NotFound;
                response.HasError = true;
            }

            return response;
        }

        public async Task<EmptyResultDto> Add(ProductDto productDto)
        {
            var result = new EmptyResultDto { Errors = null, HasError = false };
            if (productDto != null)
            {
                await unitOfWork.ProductRepository.Add(new Product
                {
                    Title = productDto.Title,
                    Price = productDto.Price,
                    Photo = productDto.Photo,
                    ProductDesc = productDto.ProductDesc,
                    CreatedDate = DateTime.Now,
                    ProductCategory = (short)productDto.ProductCategory,
                    NumberOfSeat = productDto.ProductCategory == ProductCategory.Sofa ? productDto.NumberOfSeat : (short)0
                });

                await unitOfWork.SaveChanges();
            }
            else
            {
                result.ErrorType = ErrorType.InvalidRequest;
                result.HasError = true;
            }
            return result;
        }
    }
}
