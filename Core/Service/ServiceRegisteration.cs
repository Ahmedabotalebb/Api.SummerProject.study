using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstrastion;

namespace Service
{
    public static class ServiceRegisteration
    {
            //Services.AddScoped<IServiceManager, ServiceManager>();
            //Services.AddAutoMapper(X=>X.AddProfile(new ProductProfile()));  //we need to add each profile we will do
          
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()));  //we need to add each profile we will do

            return Services;
        }
    }
}
