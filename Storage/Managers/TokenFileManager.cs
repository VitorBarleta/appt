using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using appt.Models;
using Newtonsoft.Json;
using System.Linq;

namespace appt.Storage.Managers
{
    public class TokenFileManager : ITokenFileManager
    {
        private const string TokenFileName = "appt_tokens.json";

        private readonly string _tokenFilePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                TokenFileName);

        public TokenFileManager()
        {
            if (!File.Exists(_tokenFilePath))
            {
                try
                {
                    File.Create(_tokenFilePath);
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Token>> GetTokensAsync()
        {
            var st = new StreamReader(_tokenFilePath);
            var tokensText = await st.ReadToEndAsync();
            var tokens = JsonConvert.DeserializeObject<IEnumerable<Token>>(tokensText);

            st.Close();
            st.Dispose();

            return tokens;
        }

        public async Task AddToken(Token token)
        {
            var tokens = await GetTokensAsync();

            string tokensText = string.Empty;

            if (tokens == null || !tokens.Any())
            {
                tokensText = JsonConvert.SerializeObject(new Token[] { token });
            }
            else
            {
                tokensText = JsonConvert.SerializeObject(tokens.Append(token));
            }

            var file = File.Open(_tokenFilePath, FileMode.Truncate);

            await file.WriteAsync(ASCIIEncoding.ASCII.GetBytes(tokensText));
            await file.FlushAsync();
            file.Close();
        }
    }
}