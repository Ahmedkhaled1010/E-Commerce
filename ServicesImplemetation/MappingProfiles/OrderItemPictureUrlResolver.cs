using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDtos;
using Shared.DataTransferObject.ProductModuleDto;

namespace ServicesImplemetation.MappingProfiles
{
     class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
