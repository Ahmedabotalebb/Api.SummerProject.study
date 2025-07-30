
using Api.SummerProject.study.CustomMiddleWare;
using Api.SummerProject.study.Extentions;
using Api.SummerProject.study.Factories;
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstrastion;
using Shared.ErrorModels;

namespace Api.SummerProject.study
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Add Services To The Container

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerServices();

            builder.Services.AddApplicationService();
            builder.Services.AddInfrastructureService(builder.Configuration);

            builder.Services.AddWebApplicationServices();
         
            #endregion

            var app = builder.Build();  

            try
            {
                await app.SeedDataAsync();

            }
            catch (Exception)
            {

                //TODO
            }

            // Configure the HTTP request pipeline.

            app.UseCustumMiddelWareException();

            app.UseSwaggerMiddelWares();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
