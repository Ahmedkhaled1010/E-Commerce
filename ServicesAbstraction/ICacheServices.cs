using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ICacheServices
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string cachekey, object cachevalue,TimeSpan timeSpan);
    }
}
