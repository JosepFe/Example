using JosepApp.Business.ExampleManagement.Service;
using JosepApp.Configuration.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JosepApp.Configuration
{
    public static class Configuration
    {
        public static void ConfigureApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetUpJwt(ref configuration);
            services.SetUpServices();
        }

        private static void SetUpServices(this IServiceCollection services)
        {
            services.AddTransient<IExampleService, ExampleService>();
        }
    }
}
