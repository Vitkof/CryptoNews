using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Auth
{
    public interface IJwtAuthManager
    {
        Task<JwtAuthResult> GenerateTokens(string email, Claim[] claims);
    }
}
