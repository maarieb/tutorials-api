using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using tutorialAPI.Repositories;
using tutorialAPI.Models;
using tutorialAPI.DTO;

namespace tutorialAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //fichier à mettre dans le gitignore
        private readonly IConfiguration? _configuration = null;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        //on ne peut pas comparer deux même passwords encryptés -> retourne false
        //il faut utiliser Verify
        public User? CheckUser(UserDTO userDto)
        {
            return _userRepository.FindByUsernameAndPassword(userDto.Username, userDto.Password);
        }

        //on va utiliser _configuration pour récupérer la clé
        //les données qu'on renvoie = claim
        public string GenerateJWT(User user)
        {
            string key = _configuration["JWT:Key"];

            //symmetric pour encodage et désencodage
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //données envoyées dans le jeton = claims
            Claim idClaim = new ("id", user.Id.ToString());
            Claim usernameClaim = new("username", user.Username);
            var token = new JwtSecurityToken(
                    signingCredentials: credentials,
                    claims: [idClaim, usernameClaim]
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
