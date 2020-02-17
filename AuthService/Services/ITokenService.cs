using AuthService.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface ITokenService
    {
        Task<RefreshToken> CreateTokenAsync(RefreshToken token);
        Task<RefreshToken> GetTokenAsync(string token, string login);
    }
}
