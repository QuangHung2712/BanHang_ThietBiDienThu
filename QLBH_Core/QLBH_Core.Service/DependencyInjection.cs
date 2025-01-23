using Microsoft.Extensions.DependencyInjection;
using QLBH_Core.Service.Auth;
using QLBH_Core.Service.Order;
using QLBH_Core.Service.ProductS;
using QLBH_Core.Service.ProductTypeS;

namespace QLBH_Core.Service
{
    public static class DependencyInjection
    {
        public static void ServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
        }
    }
}
