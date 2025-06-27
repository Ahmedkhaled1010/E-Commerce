using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.IdentityDto;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public AddressDto shipToAddress  { get; set; } = default!;
    }
}
