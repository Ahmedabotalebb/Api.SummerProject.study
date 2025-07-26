
using Api.SummerProject.study.CustomMiddleWare;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbcontext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            builder.Services.AddAutoMapper(X=>X.AddProfile(new ProductProfile()));  //we need to add each profile we will do
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(E => E.Value.Errors.Any())
                    .Select(M => new ValidationError()
                    {
                        Field=M.Key,
                        Errors=M.Value.Errors.Select(E=>E.ErrorMessage)
                    });
                    var response = new ValidationToReturn()
                    {
                        ValidationErrors = Errors
                    };
                    return new BadRequestObjectResult(response);
                };
                 
                
            });
            var app = builder.Build();




            try
            {
                using var scoope = app.Services.CreateScope();

                var ObjectOfDataSeeding = scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await ObjectOfDataSeeding.DataSeedAsync();

            }
            catch (Exception)
            {

                //TODO
            }

            // Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
