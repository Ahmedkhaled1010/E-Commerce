﻿using AutoMapper;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation.MappingProfiles
{
    public class PictureUrlReselover(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
       
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }
        }
    }
}
