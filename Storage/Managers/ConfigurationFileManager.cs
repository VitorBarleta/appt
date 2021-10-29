using System.Text;
using System;
using System.IO;
using System.Threading.Tasks;
using appt.Storage.Models;
using Newtonsoft.Json;

namespace appt.Storage.Managers
{
    public class ConfigurationFileManager : IConfigurationFileManager
    {
        private const string FileName = "appt_config.json";

        private readonly string _configFilePath = 
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                FileName);

        public ConfigurationFileManager()
        {
            if (!File.Exists(_configFilePath))
            {
                try
                {
                    File.Create(_configFilePath);
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<AppConfiguration> GetConfigurationFromFile()
        {
            var st = new StreamReader(_configFilePath);
            var configText = await st.ReadToEndAsync();
            var appConfiguration = JsonConvert.DeserializeObject<AppConfiguration>(configText);

            st.Close();
            st.Dispose();

            return appConfiguration;
        }

        public async Task OverwriteConfiguration(AppConfiguration appConfiguration)
        {
            var configText = JsonConvert.SerializeObject(appConfiguration);
            var file = File
                .Open(_configFilePath, FileMode.Truncate);
            
            await file.WriteAsync(ASCIIEncoding.ASCII.GetBytes(configText));
            await file.FlushAsync();
            file.Close();
        }
    }
}