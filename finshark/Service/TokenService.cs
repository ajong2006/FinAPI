using finshark.Interfaces;
using finshark.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace finshark.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])); //Have to turn it into bytes
        }
        public string CreateToken(AppUser user)
        {
            //You can use claims to identify the user and what they can and cant do
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            //Determines the type fo encryption you want
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Create the token as an object
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            //Create a new tokenHandler which will what will create the actual token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Generate the actual token
            var token = tokenHandler.CreateToken(tokenDescripor);

            //return token as string
            return tokenHandler.WriteToken(token);
        }
    }
}
