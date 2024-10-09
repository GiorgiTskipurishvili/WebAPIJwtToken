using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Model;

namespace WebAPI.Auth
{

    public interface IJwtManager
    {
        Token GetToken(User user);
    }
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;

        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            //
            //var userRole = user.Username == "admin" ? "Admin" : "User";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username",user.Username, ClaimTypes.Name),
                    new Claim("password",user.Password, ClaimTypes.NameIdentifier),
                    new Claim("role",user.Role.ToString(),ClaimTypes.Role)
                    //
                    //new Claim(ClaimTypes.Name, user.Username),
                    //new Claim(ClaimTypes.Role,userRole)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var tokenData = tokenHandler.CreateToken(tokenDescriptor);
            var token = new Token { AccessToken = tokenHandler.WriteToken(tokenData) };
            return token;
        }

    }


    public class Token
    {
        public string? AccessToken { get; set; }
    }


}
