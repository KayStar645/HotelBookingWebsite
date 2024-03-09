using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Profiles;
using Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppllicatgionServices(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommonMappingProfile());
                cfg.AddProfile(new ModuleMappingProfile());
            }).CreateMapper());

            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IKindRoomService, KindRoomService>();
            services.AddScoped<IRoomService, RoomService>();

            return services;
        }
    }
}
