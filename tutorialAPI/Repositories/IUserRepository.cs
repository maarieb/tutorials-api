using tutorialAPI.Models;

namespace tutorialAPI.Repositories
{
    public interface IUserRepository
    {
        User? FindByUsernameAndPassword(string username, string password);
    }
}
