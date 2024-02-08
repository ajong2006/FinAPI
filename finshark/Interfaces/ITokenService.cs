using finshark.Models;

namespace finshark.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
