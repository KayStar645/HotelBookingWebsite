using AutoMapper;
using Core.Application;
using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Auth;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Presentation.Web;
using Presentation.Web.Middleware;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
                        ValidAudience = builder.Configuration.GetValue<string>("JwtSettings:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                    };
                });
builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPermissionPoliciesFromAttributes(Assembly.GetExecutingAssembly());
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<ExceptionMiddleware>();

// Add services to the container.

builder.Services.AddAppllicatgionServices();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddWebServices();

builder.Services.AddControllersWithViews()
        .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

	// Initialise and seed database
	using var scope = app.Services.CreateScope();
	var initializer = scope.ServiceProvider.GetRequiredService<HotelBookingWebsiteDbContextInitialiser>();
	await initializer.InitializeAsync();
	await initializer.SeedAsync();
	InitializePermissions(builder.Services.BuildServiceProvider()).GetAwaiter().GetResult();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();


async Task InitializePermissions(IServiceProvider serviceProvider)
{
	var permissionService = serviceProvider.GetRequiredService<IPermissionService>();

    List<string> permissions = AuthorizationExtensions
            .GetPermissionPoliciesFromAttributes(Assembly.GetExecutingAssembly());
	await permissionService.Create(permissions);
}