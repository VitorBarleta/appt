using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace appt.Options.Extensions
{   
    public static class StartupOptionsExtensions
    {
        public static void AddAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccountOptions>(configuration.GetSection(nameof(AccountOptions)));
        }
    }
}