
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;
using E_Commerce.Web.Extention;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using ServicesAbstraction;
using ServicesImplemetation;
using ServicesImplemetation.MappingProfiles;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
           builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            builder.Services.AddJWTServices(builder.Configuration); 
            var app = builder.Build();
            await app.SeedDataBaseAsync();
            app .UseCustomExceptionMiddleWare();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;

                if (path != null && path.StartsWith("/images"))
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                }

                await next();
            });
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
