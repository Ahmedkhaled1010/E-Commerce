using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attribute;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObject.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Authorize]
    public class ProductsController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProductsAsync([FromQuery]ProductQueryParams queryParams)
        {

            var products = await servicesManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto> > GetProductByIdAsync(int id)
        {
            var product =await servicesManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types =await servicesManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await servicesManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
