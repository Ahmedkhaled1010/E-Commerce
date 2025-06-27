using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation
{
    public class ServiceManager(IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketRepository basketRepository,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration) 
    {
        private readonly Lazy<IProductService> LazyProductService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        private readonly Lazy<IAuthenticationServices> LazyAuthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager,configuration,mapper));
        private readonly Lazy<IBasketServices> LazyBasketService = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        private readonly Lazy<IOrderServices> LazyOrderServices = new Lazy<IOrderServices>(() => new OrderServices(mapper, basketRepository, unitOfWork));
        public IProductService ProductService => LazyProductService.Value;

        public IBasketServices BasketService => LazyBasketService.Value;
        public IAuthenticationServices AuthenticationServices => LazyAuthenticationServices.Value;

        public IOrderServices OrderServices => LazyOrderServices.Value;
    }
}
