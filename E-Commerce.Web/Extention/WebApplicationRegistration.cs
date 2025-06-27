using System.Text.Json;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web.Extention
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var DataSeedObject = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedObject.DataSeedAsync();
            await DataSeedObject.IdentityDataSeedAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options=>
            {

                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration=true,
                };
                options.DocumentTitle = "E-Commerce";
                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();

               
            });
            return app;
        }
    }
}
