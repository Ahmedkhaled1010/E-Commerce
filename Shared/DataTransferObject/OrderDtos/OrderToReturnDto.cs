using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.IdentityDto;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid  Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset orderDate { get; set; } 
        public AddressDto shipToAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string status { get; set; } = default!;
        public ICollection<OrderItemDto> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal Total { get; set; }


    }
}
