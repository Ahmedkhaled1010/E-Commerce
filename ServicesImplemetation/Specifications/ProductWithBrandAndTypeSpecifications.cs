using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation.Specifications
{
    class ProductWithBrandAndTypeSpecifications :BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            :base(p=>(!queryParams.BrandId.HasValue||p.ProductBrandId== queryParams.BrandId)
            &&(!queryParams.TypeId.HasValue||p.ProductTypeId== queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.search)||p.Name.ToLower().Contains(queryParams.search.ToLower())) )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.sort) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p=>p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
