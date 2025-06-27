using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServicesAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace ServicesImplemetation
{
    public class BasketServices(IBasketRepository basketRepository,IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Basket = mapper.Map<BasketDto,CustomerBasket>(basket);
         var IsCreatedOrUpdated =await   basketRepository.CreateOrUpdateBasketAsync(Basket);
            if (IsCreatedOrUpdated is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Can Not Update Or Create Basket Now ,Try Again Later");
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
          return await basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket =await basketRepository.GetBasketAsync(key);
            if (Basket is not null)
            {
                return mapper.Map<CustomerBasket, BasketDto>(Basket);

            }
            else
            {
                throw new BasketNotFoundException(key);
            }
        }
    }
}
