using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWare
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next,ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something Went Wrong");
                //Set Status Code For Response
                //httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
           

            //Set Content Type For Response
            httpContext.Response.ContentType = "application/json";

            //Respone Object
            var Response = new ErrorToReturn()
            {
              //  StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequest => GetBadRequestErrors(badRequest, Response),
                _ => StatusCodes.Status500InternalServerError
            };
            //Return Object As JSON
            //  var ResponseToReturn=JsonSerializer.Serialize(Response);

            //await  httpContext.Response.WriteAsync(ResponseToReturn);
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequest, ErrorToReturn response)
        {
            response.Errors=badRequest.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
