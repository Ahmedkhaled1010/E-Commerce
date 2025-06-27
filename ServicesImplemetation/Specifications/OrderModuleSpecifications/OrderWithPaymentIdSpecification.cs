using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation.Specifications.OrderModuleSpecifications
{
    internal class OrderWithPaymentIdSpecification:BaseSpecifications<Order,Guid>

    {
        public OrderWithPaymentIdSpecification(string PaymentIntentId):base(o=>o.paymentIntentId ==PaymentIntentId)
        {
            
        }
    }
}
