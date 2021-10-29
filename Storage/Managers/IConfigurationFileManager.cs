using System.Threading.Tasks;
using appt.Storage.Models;

namespace appt.Storage.Managers
{
    public interface IConfigurationFileManager
    {
        Task<AppConfiguration> GetConfigurationFromFile();
        Task OverwriteConfiguration(AppConfiguration appConfiguration);
    }
}