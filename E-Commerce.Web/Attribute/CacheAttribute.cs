using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServicesAbstraction;
using ServicesImplemetation;

namespace E_Commerce.Web.Attribute
{
    public class CacheAttribute(int DurationInSecond =90) :ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CacheKey=CreateCacheKey(context.HttpContext.Request);
            ICacheServices cacheServices =context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            var CacheValue =await cacheServices.GetAsync(CacheKey);
            if (CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
           var ExcecutedContext =await next.Invoke();

            if (ExcecutedContext.Result is OkObjectResult result)
            {
              await  cacheServices.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSecond));

            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key=new StringBuilder();
            key.Append(request.Path +'?');
            foreach (var item in request.Query.OrderBy(q=>q.Key)) 
            { 
                key.Append($"{item.Key}={item.Value}$");
            }
            return key.ToString();
        }
    }
}
