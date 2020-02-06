using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using WebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using WebAPILibrary;
using WebAPILibrary.Models;

namespace WebAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Login(string username, string pass)
        {
            WebAPILibrary.Models.UserModel login = new WebAPILibrary.Models.UserModel
            {
                UserName = username,
                Password = pass
            };
            IActionResult response = Unauthorized();

            UserAuthetication userAuth = new UserAuthetication(_config);
            
            var user = userAuth.AuthenticateUser(login);

            if (user != null)
            {
                string tokenstr = userAuth.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenstr });
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to: " + userName;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}
