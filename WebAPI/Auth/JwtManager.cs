using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Model;

namespace WebAPI.Auth
{

    public interface IJwtManager
    {
        Token GetToken(Login user);
    }
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;

        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GetToken(Login user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username",user.Username, ClaimTypes.Name),
                    new Claim("password",user.Password, ClaimTypes.NameIdentifier)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var tokenData = tokenHandler.CreateToken(tokenDescriptor);
            var token = new Token { AccessToken = tokenHandler.WriteToken(tokenData) };
            return token;
        }

    }



    //public class JwtManager 
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly string _key;

    //    public JwtManager(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //        _key = _configuration["JWT:Key"];
    //        if (string.IsNullOrEmpty(_key))
    //        {
    //            throw new ArgumentNullException("JWT Key is missing from configuration.");
    //        }
    //    }

    //    public string Authenticate(string username, string password)
    //    {
    //        if (!(username == "admin" && password == "admin")) // Replace this with proper authentication logic
    //            return null;

    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_key);
    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Expires = DateTime.UtcNow.AddDays(1),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };

    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        return tokenHandler.WriteToken(token);
    //    }
    //}



}
