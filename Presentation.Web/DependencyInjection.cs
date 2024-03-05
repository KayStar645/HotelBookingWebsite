using Core.Application.Interfaces;
using Presentation.Web.Services;

namespace Presentation.Web
{
    public static class DependencyInjection
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
