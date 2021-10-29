using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace appt
{
    class Program
    {
        static async Task Main(string[] args) => 
            await HostBuilder
                .BuildConfigurationBuilder()
                .UseStartup<Startup>()
                .RunAsync<App>(args);
    }

    public interface IApp
    {
        Task RunAsync(string[] args);
    }

    public static class HostBuilder
    {
        public static Starter BuildConfigurationBuilder()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", false, true);
            return new Starter(configBuilder.Build());
        }

        public class Starter
        {
            private readonly IConfiguration _configuration;
            private readonly IServiceCollection _serviceCollection;

            public Starter(IConfiguration configuration)
            {
                _configuration = configuration;
                _serviceCollection = new ServiceCollection();
            }

            public Runner UseStartup<T>() where T : class
            {
                var type = typeof(T);
                var constr = type.GetConstructor(new Type[] { typeof(IConfiguration) });

                var startup = constr.Invoke(new[] { _configuration });

                var configServicesMethod = startup.GetType().GetMethod("ConfigureServices");
                configServicesMethod.Invoke(startup, new[] { _serviceCollection });

                return new Runner(_serviceCollection);
            }
        }

        public class Runner
        {
            private readonly IServiceCollection _services;

            public Runner(IServiceCollection services)
            {
                _services = services;
            }

            public Task RunAsync<T>(string[] args) where T : class, IApp
            {
                _services.AddSingleton(typeof(T));

                return _services.BuildServiceProvider().GetService<T>().RunAsync(args);
            }
        }
    }
}
