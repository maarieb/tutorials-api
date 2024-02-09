

using tutorialAPI.DTO;
using tutorialAPI.Models;

namespace tutorialAPI.Services
{
    public interface IAuthenticationService
    {
        User? CheckUser(UserDTO userDto);
        string GenerateJWT(User user);

    }
}
