using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;

namespace ServicesImplemetation
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IServicesManager, ServiceManagerWithFactoryDelegate>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());
            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<Func<IOrderServices>>(provider =>
            () => provider.GetRequiredService<IOrderServices>());
            Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            Services.AddScoped<Func<IAuthenticationServices>>(provider =>
            () => provider.GetRequiredService<IAuthenticationServices>());
            Services.AddScoped<IBasketServices, BasketServices>();
            Services.AddScoped<Func<IBasketServices>>(provider =>
            () => provider.GetRequiredService<IBasketServices>());
            Services.AddAutoMapper(typeof(ServicesImplemetation.AssemblyReference).Assembly);
            Services.AddScoped<ICacheServices, CacheServices>();
            Services.AddScoped<IPaymentServices, PaymentServices>();
            Services.AddScoped<Func<IPaymentServices>>(provider =>
            () => provider.GetRequiredService<IPaymentServices>());
            return Services;
        }
    }
}
