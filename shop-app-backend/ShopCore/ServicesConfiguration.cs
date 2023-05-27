using Microsoft.Extensions.DependencyInjection;

namespace ShopCore
{
    public static class ServicesConfiguration
    {
        public static void AddShopCore(this IServiceCollection services)
        {
            services.AddScoped<Categories>();
            services.AddScoped<Products>();
        }
    }
}
