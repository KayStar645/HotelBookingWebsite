using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.GoogleDrive;
using Core.Application.Profiles;
using Core.Application.Services;
using Core.Application.Services.Auth;
using Core.Application.Services.GoogleDrive;
using Core.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class DependencyInjection
    {
		public static IServiceCollection AddAppllicatgionServices(this IServiceCollection services)
		{
			services.AddTransient<IGoogleDriveService, GoogleDriveService>();

			services.AddSingleton(provider => new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new CommonMappingProfile(provider.GetService<IGoogleDriveService>()));
				cfg.AddProfile(new ModuleMappingProfile());
			}).CreateMapper());

			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

			services.AddScoped<IStaffService, StaffService>();
			services.AddScoped<IKindRoomService, KindRoomService>();
			services.AddScoped<IRoomService, RoomService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IServiceService, ServiceService>();
			services.AddScoped<ICustomerService, CustomerService>();
      
			return services;
		}

	}
}
