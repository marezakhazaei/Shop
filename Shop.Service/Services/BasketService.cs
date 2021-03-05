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
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork unitOfWork;

        public BasketService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<BasketItemCountResultDto> Add(ProductAddDto product)
        {
            var result = new BasketItemCountResultDto();
            if (product != null)
            {
                var clientId = product.ClientId;
                var basketFind = await unitOfWork.BasketRepository.GetDetailBasketItems(clientId);
                var productFind = await unitOfWork.ProductRepository.Get(product.ProductId);
                if (basketFind == null)
                {
                    result = await AddNewBasket(product, productFind);
                }
                else
                {
                    result = await AddToExistingBasket(basketFind, product, productFind);
                }
            }
            else
            {
                result.ErrorType = ErrorType.InvalidRequest;
                result.HasError = true;
            }
            return result;
        }

        public async Task<BasketItemCountResultDto> CountBasketItems(string clientId)
        {
            var result = new BasketItemCountResultDto();
            if (!string.IsNullOrEmpty(clientId))
            {
                var count = await unitOfWork.BasketItemRepository.GetBasketItemCount(clientId);
                result.TotalCount = count;
            }
            else
            {
                result.ErrorType = ErrorType.InvalidRequest;
                result.HasError = true;
            }
            return result;
        }

        public async Task<EmptyResultDto> Delete(long basketItemId)
        {
            var result = new EmptyResultDto { Errors = null, HasError = false };
            if (basketItemId != 0)
            {
                var basketItem = await unitOfWork.BasketItemRepository.Get(basketItemId);
                if (basketItem != null)
                {
                    var basket = await unitOfWork.BasketRepository.Get(basketItem.BasketId);
                    basket.TotalCount -= basketItem.ProductCount;
                    basket.ProductPrices -= basketItem.Price;
                    basket.TotalPrice = basket.ProductPrices;
                    basket.TotalDiscount = 0;
                    await unitOfWork.BasketItemRepository.Delete(basketItemId);
                    await unitOfWork.SaveChanges();
                }
            }
            else
            {
                result.ErrorType = ErrorType.InvalidRequest;
                result.HasError = true;
            }
            return result;
        }

        public async Task<BasketDto> Get(string clientId)
        {
            var response = new BasketDto();
            if (!string.IsNullOrEmpty(clientId))
            {
                var result = await unitOfWork.BasketRepository.GetDetailBasketItems(clientId);
                if (result != null)
                    response = new BasketDto
                    {
                        TotalDiscount = result.TotalDiscount,
                        ProductPrices = result.ProductPrices,
                        TotalPrice = result.TotalPrice,
                        BasketItems = result.BasketItems.OrderByDescending(o => o.Price).Select(b => new BasketItemDto
                        {
                            Basketid = b.BasketId,
                            Id = b.Id,
                            Price = b.Price,
                            ProductCount = b.ProductCount,
                            ProductId = b.ProductId,
                            ProductPhoto = b.Product.Photo,
                            ProductPrice = b.Product.Price,
                            ProductTitle = b.Product.Title
                        })
                    };
                response.TotalItem = response.BasketItems != null ? response.BasketItems.Sum(s => s.ProductCount) : 0;
            }
            else
            {
                response.ErrorType = ErrorType.InvalidRequest;
                response.HasError = true;
            }

            return response;
        }

        public async Task<BasketPriceDto> CaculateDiscount(string clientId, bool isStaff)
        {
            var response = new BasketPriceDto();
            if (!string.IsNullOrEmpty(clientId))
            {
                var basket = await unitOfWork.BasketRepository.GetDetailBasketItems(clientId);
                if (basket != null)
                {
                    response = await SetDiscount(basket, isStaff);

                }
            }
            else
            {
                response.ErrorType = ErrorType.InvalidRequest;
                response.HasError = true;
            }
            return response;
        }

        private async Task<BasketPriceDto> SetDiscount(Basket basket, bool isStaff)
        {
            var response = new BasketPriceDto();
            if (basket != null)
            {
                response.TotalDiscount = basket.TotalDiscount;
                response.TotalPrice = basket.TotalPrice;
                var basketItems = basket.BasketItems.Select(s => new ProductBasketItem
                {
                    Count = s.ProductCount,
                    NumberOfSeat = s.Product.NumberOfSeat ?? 0,
                    Price = s.Product.Price,
                    ProductCategory = (ProductCategory)s.Product.ProductCategory
                }).ToList();
                var staffDiscount = DiscountFactory.GetDicsount(basketItems, isStaff);
                response.TotalDiscount = staffDiscount;
                response.TotalPrice = basket.ProductPrices - response.TotalDiscount;
                basket.TotalDiscount = response.TotalDiscount;
                basket.TotalPrice = response.TotalPrice;

                await unitOfWork.SaveChanges();
            }
            else
            {
                response.ErrorType = ErrorType.InvalidRequest;
                response.HasError = true;
            }
            return response;
        }

        private async Task<BasketItemCountResultDto> AddNewBasket(ProductAddDto product, Product productFind)
        {
            var result = new BasketItemCountResultDto();
            var date = DateTime.Now;
            var totalPrice = product.Count * productFind.Price;
            var basket = new Basket
            {
                ClientId = product.ClientId,
                BasketStatus = (short)BasketStatus.Active,
                CreatedDate = date,
                TotalCount = product.Count,
                TotalPrice = totalPrice,
                ProductPrices = totalPrice,
                BasketItems = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            ProductId=product.ProductId,
                            ProductCount=product.Count,
                            CreatedDate=date,
                            Price=totalPrice,
                        }
                    }
            };
            await unitOfWork.BasketRepository.Add(basket);

            await unitOfWork.SaveChanges();
            result.TotalCount = basket.TotalCount;

            return result;
        }

        private async Task<BasketItemCountResultDto> AddToExistingBasket(Basket basketFind, ProductAddDto product, Product productFind)
        {
            var result = new BasketItemCountResultDto();
            var date = DateTime.Now;
            var price = 0m;
            var basketItem = basketFind.BasketItems.Where(o => o.ProductId == productFind.Id).FirstOrDefault();
            if (basketItem != null)
            {
                basketItem.ProductCount += product.Count;
                basketItem.Price = basketItem.ProductCount * productFind.Price;
                price = productFind.Price * product.Count;
            }
            else
            {
                price = product.Count * productFind.Price;
                await unitOfWork.BasketItemRepository.Add(new BasketItem
                {
                    BasketId = basketFind.Id,
                    ProductId = product.ProductId,
                    ProductCount = product.Count,
                    CreatedDate = date,
                    Price = price,
                });
            }

            basketFind.TotalCount += product.Count;
            basketFind.ProductPrices += price;
            basketFind.TotalPrice = basketFind.ProductPrices;
            basketFind.TotalDiscount = 0;

            await unitOfWork.SaveChanges();

            result.TotalCount = basketFind.TotalCount;

            return result;
        }

    }
}
