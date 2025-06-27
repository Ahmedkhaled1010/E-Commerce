using Shared.DataTransferObject.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IPaymentServices
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId);
    }
}
