using FiorellaApp.Data;
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
        }
    }
}
