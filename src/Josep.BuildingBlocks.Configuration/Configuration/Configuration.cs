using JosepApp.BuildingBlocks.Configuration.Configuration.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JosepApp.BuildingBlocks.Configuration.Configuration
{
    public static class Configuration
    {
        public static void ConfigureApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetUpJwt(ref configuration);
        }
    }
}
