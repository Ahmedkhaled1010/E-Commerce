using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using ServicesAbstraction;
using ServicesImplemetation.Specifications.OrderModuleSpecifications;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;

namespace ServicesImplemetation
{
    public class OrderServices(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto order, string Email)
        {
            var OrderAddress = mapper.Map<AddressDto,OrderAddress>(order.shipToAddress);
            var Basket =await basketRepository.GetBasketAsync(order.BasketId) ?? throw new BasketNotFoundException(order.BasketId);
            ArgumentNullException.ThrowIfNullOrEmpty(Basket.paymentIntentId);
            var OrderRepo = unitOfWork.GetGenericRepository<Order, Guid>();
            var OrderSpec = new OrderWithPaymentIdSpecification(Basket.paymentIntentId);
            var ExistingOrder =await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrder is not null)
            {
                OrderRepo.Remove(ExistingOrder);
            }
            List<OrderItem> orderItems = new List<OrderItem>();
            var ProductRepo = unitOfWork.GetGenericRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, Product));

            }
            var DeliveryMethod =await unitOfWork.GetGenericRepository<DeliveryMethods, int>().GetByIdAsync(order.DeliveryMethodId)??
                throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
                ;

            var SubTotal = orderItems.Sum(i => i.Quantity * i.Price);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal,Basket.paymentIntentId);
           await OrderRepo.AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<Order, OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                Price = Product.Price,
                Quantity = item.Quantity,
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var DeliverMethod =await unitOfWork.GetGenericRepository<DeliveryMethods,int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethods>,IEnumerable<DeliveryMethodDto>>(DeliverMethod);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string email)
        {
            var spec = new OrderSpecifiaction(email);
            var orders =await unitOfWork.GetGenericRepository<Order,Guid>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderById(Guid id)
        {
            var spec = new OrderSpecifiaction(id);
            var Order = await unitOfWork.GetGenericRepository<Order,Guid>().GetByIdAsync(spec);
            return mapper.Map<OrderToReturnDto>(Order); 
        }
    }
}
