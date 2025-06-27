using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObject.BasketModuleDto;

namespace Presentation.Controller
{
  
    public class BasketController(IServicesManager servicesManager):ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto> > GetBasket(string key) 
        
        {
                var Basket =await servicesManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }
        [HttpPost]
        public async  Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Basket =await servicesManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Res =await servicesManager.BasketService.DeleteBasketAsync(key);
            return Ok(Res);
        }
    }
}
