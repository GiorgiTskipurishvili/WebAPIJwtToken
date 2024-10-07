using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Package;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //[HttpGet]
        //public List<User> GetUsers()
        //{
        //    PKG_USER package = new PKG_USER();
        //    List<User> list_users = new List<User>();
        //    list_users = package.get_users();
        //    return list_users;
        //}


        [HttpPost]
        public void SaveUser(User user)
        {
            PKG_USER package = new PKG_USER();
            package.saveUser(user);
        }


        //[HttpGet("{id}")]
        //public User GetUserByID(int id)
        //{
        //    PKG_USER package = new PKG_USER();
        //    User user = new User();
        //    user.ID = id;
        //    return package.get_user_by_id(user);
        //}

        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            PKG_USER userPKG = new PKG_USER();
            User user = userPKG.getUserByUsername(username);

            return Ok(user);
        }


    }
}
