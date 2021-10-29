using System.Collections.Generic;
using System.Threading.Tasks;
using appt.Models;

namespace appt.Storage.Managers
{
    public interface ITokenFileManager
    {
        Task<IEnumerable<Token>> GetTokensAsync();
        Task AddToken(Token token);
    }
}