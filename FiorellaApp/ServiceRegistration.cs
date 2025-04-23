using FiorellaApp.Data;
using FiorellaApp.Services;
using FiorellaApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorellaApp
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services,IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<FiorelloDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("FioDatabase"));
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService, BasketService>();
        }
    }
}
