using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstraction;

namespace ServicesImplemetation
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> IProduct,
        Func<IBasketServices> IBasket,
        Func<IOrderServices> IOrder,
        Func<IAuthenticationServices> IAuth,
        Func<IPaymentServices> IPay) : IServicesManager
    {
        public IProductService ProductService => IProduct.Invoke();

        public IBasketServices BasketService => IBasket.Invoke();

        public IAuthenticationServices AuthenticationServices => IAuth.Invoke();

        public IOrderServices OrderServices => IOrder.Invoke();
        public IPaymentServices paymentServices =>IPay.Invoke();
    }
}
