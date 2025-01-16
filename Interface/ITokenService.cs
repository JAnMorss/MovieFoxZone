using MovieApi.Models;

namespace MovieApi.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
