using Core.Application.Interfaces;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<EntitySaveChangesInterceptor>();

            services.AddDbContext<IHotelBookingWebsiteDbContext, HotelBookingWebsiteDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("HBWConnect"), builder =>
                {
                    builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                    builder.EnableRetryOnFailure();
                }));

            services.AddScoped<HotelBookingWebsiteDbContextInitialiser>();

            services.AddSingleton<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
