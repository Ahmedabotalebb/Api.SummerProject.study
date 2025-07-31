using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbcontext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
            });

            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
               return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });

            Services.AddDbContext<StoreIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            return Services;
        }        
        }
}
