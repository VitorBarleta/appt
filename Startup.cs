using appt.Options.Extensions;
using appt.Processors;
using appt.Processors.Contracts;
using appt.Storage;
using appt.Storage.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace appt
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettingsOptions(Configuration);

            services.AddScoped<IApplicationContext, ApplicationContext>();
            
            services.AddScoped<IArgumentProcessor, Message>();
            services.AddScoped<IArgumentProcessor, Configuration>();

            services.AddSingleton<IConfigurationFileManager, ConfigurationFileManager>();
            services.AddSingleton<ITokenFileManager, TokenFileManager>();

        }
    }
}
