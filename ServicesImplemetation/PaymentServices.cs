using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using Shared.DataTransferObject.BasketModuleDto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModule.Product;
namespace ServicesImplemetation
{
    public class PaymentServices(IConfiguration configuration,IBasketRepository basket,
        IUnitOfWork unitOfWork,IMapper mapper) : IPaymentServices
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var Basket  =await basket.GetBasketAsync(BasketId)??throw new BasketNotFoundException(BasketId);

            var ProductRepo =unitOfWork.GetGenericRepository<Product,int>();
            foreach (var item in Basket.Items)
            {
                var product =await ProductRepo.GetByIdAsync(item.Id)??throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DeliveryMethod =await unitOfWork.GetGenericRepository<DeliveryMethods,int>().GetByIdAsync(Basket.deliveryMethodId.Value)?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);
            Basket.shippingPrice = DeliveryMethod.Cost;
            var BasketAmount =(long) (Basket.Items.Sum(item => item.Quantity * item.Price)+DeliveryMethod.Cost)*100;
            var PaymentServices =new PaymentIntentService();
            if (Basket.paymentIntentId is  null)//create
            {
                var Options = new PaymentIntentCreateOptions()
                { 
                    Amount = BasketAmount,
                    Currency ="USD",
                    PaymentMethodTypes = ["card"]
                };
                //يرجعلي paymentontent
           var PaymentIntent =    await PaymentServices.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret= PaymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                { 
                    Amount= BasketAmount,
                };
               await PaymentServices.UpdateAsync(Basket.paymentIntentId,Options);
            }
            await basket.CreateOrUpdateBasketAsync(Basket); 
            return mapper.Map<BasketDto>(Basket);
        }
    }
}
