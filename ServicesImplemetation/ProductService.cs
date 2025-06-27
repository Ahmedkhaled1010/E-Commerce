using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using ServicesAbstraction;
using ServicesImplemetation.Specifications;
using Shared;
using Shared.DataTransferObject.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var BrandsRepo = unitOfWork.GetGenericRepository<ProductBrand, int>();
            var Brands = await BrandsRepo.GetAllAsync();
            var BrandsDto=mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo =  unitOfWork.GetGenericRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
           var products=await Repo.GetAllAsync(specifications);
            var Data= mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products);
            var ProductsCount =products.Count();
            var CountSpecific = new ProductCountSpecification(queryParams);
            var TotalCount =await Repo.CountAsync(CountSpecific);
            return new PaginatedResult<ProductDto>(queryParams.pageNumber,ProductsCount, TotalCount, Data);

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types =await unitOfWork.GetGenericRepository<ProductType, int>().GetAllAsync();
            var TypesDto= mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);

            var product =await unitOfWork.GetGenericRepository<Product,int>().GetByIdAsync(specifications);
            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return mapper.Map<Product, ProductDto>(product);
        }
    }
}
