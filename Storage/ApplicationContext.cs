using System.Collections.Generic;
using System.Threading.Tasks;
using appt.Models;
using appt.Storage.Managers;
using appt.Storage.Models;

namespace appt.Storage
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly IConfigurationFileManager _configurationFileManager;
        private readonly ITokenFileManager _tokenFileManager;

        private AppConfiguration _configurationCache;

        public ApplicationContext(
            IConfigurationFileManager configurationFileManager,
            ITokenFileManager tokenFileManager)
        {
            _configurationFileManager = configurationFileManager;
            _tokenFileManager = tokenFileManager;
        }

        public AppConfiguration GetConfiguration()
        {
            if (_configurationCache == null)
            {
                _configurationCache = _configurationFileManager
                    .GetConfigurationFromFile()
                    .GetAwaiter()
                    .GetResult();
            }

            return _configurationCache;
        }

        public async Task<AppConfiguration> GetConfigurationAsync()
        {
            if (_configurationCache == null)
            {
                _configurationCache = await _configurationFileManager.GetConfigurationFromFile();
            }

            return _configurationCache;
        }

        public Task OverwriteConfiguration(AppConfiguration appConfiguration) =>
            _configurationFileManager.OverwriteConfiguration(appConfiguration);

        public Task<IEnumerable<Token>> GetTokens() => _tokenFileManager.GetTokensAsync();

        public Task AddToken(Token token) => _tokenFileManager.AddToken(token);
    }
}
