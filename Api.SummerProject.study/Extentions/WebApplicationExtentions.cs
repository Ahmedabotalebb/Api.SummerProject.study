using System.Net.NetworkInformation;
using Api.SummerProject.study.CustomMiddleWare;
using Domain.Contracts;

namespace Api.SummerProject.study.Extentions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scoope = app.Services.CreateScope();

            var ObjectOfDataSeeding = scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
            return app;
        }

        public static IApplicationBuilder UseCustumMiddelWareException(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app; 
        }

        public static IApplicationBuilder UseSwaggerMiddelWares(this IApplicationBuilder app)
        {   
                app.UseSwagger();
                app.UseSwaggerUI();
 
            return app;
        }
        }
}
