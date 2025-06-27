using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModule;

namespace ServicesImplemetation.Specifications.OrderModuleSpecifications
{
     class OrderSpecifiaction:BaseSpecifications<Order,Guid>
    {
        public OrderSpecifiaction(string email):base(o=>o.buyerEmail==email)
        {
            AddInclude(o => o.DeliveryMethods);
            AddInclude(o=>o.Items);
            AddOrderByDescending(o => o.orderDate);
        }
        public OrderSpecifiaction(Guid id):base(o=>o.Id==id)  
        {
            AddInclude(o => o.DeliveryMethods);
            AddInclude(o => o.Items);
        }
    }
}
