using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation.Specifications
{
    class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams ) : base(p => (!queryParams.BrandId.HasValue || p.ProductBrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || p.ProductTypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
        }
    }
}
