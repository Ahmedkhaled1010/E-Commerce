using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObject.OrderDtos;

namespace Presentation.Controller
{
    [Authorize]

    public class OrderController(IServicesManager servicesManager):ApiBaseController
    {
        [HttpPost]
        public async Task <ActionResult<OrderToReturnDto>> CreateOrder(OrderDto order)
        {
            var Order =await servicesManager.OrderServices.CreateOrder(order, GetEmailFromToken());
            return Ok(Order);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
            var deliveryMethod =await servicesManager.OrderServices.GetDeliveryMethodAsync();
            return Ok(deliveryMethod); 
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Orders =await servicesManager.OrderServices.GetAllOrderAsync(GetEmailFromToken());
            return Ok(Orders);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order = await servicesManager.OrderServices.GetOrderById(id);
            return Ok(Order);

        }
    }
}