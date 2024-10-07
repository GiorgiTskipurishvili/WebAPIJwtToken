using Microsoft.AspNetCore.Mvc;
using WebAPI.Auth;
using WebAPI.Model;
using WebAPI.Package;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthennticationController : Controller
    {
        private readonly IJwtManager jwtManager;
        public AuthennticationController(IJwtManager jwtManager)
        {
            this.jwtManager = jwtManager;
        }




        [HttpPost]
        public IActionResult Authenticate(Login user)
        {
            PKG_USER package = new PKG_USER();
            User existingUser = package.getUserByUsername(user.Username);
            if (existingUser != null && existingUser.Password == user.Password)
            {
                var token = jwtManager.GetToken(user);
                return Ok(token);
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }

    }
}
