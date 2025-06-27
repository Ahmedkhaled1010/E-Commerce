using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObject.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class PaymentsController(IServicesManager servicesManager):ApiBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket =await servicesManager.paymentServices.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
