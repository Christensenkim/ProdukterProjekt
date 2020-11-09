using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProdukterProjekt.Core.ApplicationService;
using ProdukterProjekt.Core.Entity;
using ProdukterProjekt.UI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProdukterProjekt.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenicationHelper authenicationHelper;

        public TokenController(IUserService userService, IAuthenicationHelper authHelp)
        {
            _userService = userService;
            authenicationHelper = authHelp;
        }

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Login([FromBody] LoginModule module)
        {
            var user = _userService.ReadUsers().FirstOrDefault(u => u.userName == module.username);

            if(user == null)
            {
                return Unauthorized();
            }

            if(!authenicationHelper.VerifyPasswordHash(module.password, user.passwordHash, user.passwordSalt))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                username = user.userName,
                token = authenicationHelper.GenerateToken(user)
            });
        }
    }
}
