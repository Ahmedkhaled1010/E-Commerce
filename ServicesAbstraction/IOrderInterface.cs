using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.OrderDtos;

namespace ServicesAbstraction
{
    public interface IOrderServices
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto order, string Email);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string email);
        Task<OrderToReturnDto> GetOrderById(Guid id);

    }
}
