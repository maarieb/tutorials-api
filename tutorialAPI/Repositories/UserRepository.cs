using tutorialAPI.Configs;
using tutorialAPI.Models;

namespace tutorialAPI.Repositories
{
    //Classe puis Repository
    public class UserRepository : IUserRepository
    {
        private AppTutorialContext _appTutorialContext;

        public UserRepository(AppTutorialContext context) 
        {
            _appTutorialContext = context;
        }

        public User? FindByUsernameAndPassword(string username, string password)
        {
            var user = _appTutorialContext.Users.FirstOrDefault(elt => elt.Username.Equals(username));
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) 
            {
                return null;
            }
            return user;
        }
    }
}
