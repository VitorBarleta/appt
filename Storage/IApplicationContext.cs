using System.Collections.Generic;
using System.Threading.Tasks;
using appt.Models;
using appt.Storage.Models;

namespace appt.Storage
{
    public interface IApplicationContext
    {
        AppConfiguration GetConfiguration();
        Task<AppConfiguration> GetConfigurationAsync();
        Task OverwriteConfiguration(AppConfiguration appConfiguration);

        Task<IEnumerable<Token>> GetTokens();
        Task AddToken(Token token);
    }
}
