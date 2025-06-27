using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServicesManager
    {
        public IProductService ProductService { get; }
        public IBasketServices BasketService { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IOrderServices OrderServices { get; }
        public IPaymentServices paymentServices { get; }
    }
}
