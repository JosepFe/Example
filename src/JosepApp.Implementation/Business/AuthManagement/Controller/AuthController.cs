using JosepApp.BuildingBlocks.Configuration.Configuration.JWT.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace JosepApp.Implementation.Business.AuthManagement.Controller
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtHandler _jwtHandler;

        public AuthController(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpGet]
        [Authorize(Policy = "PostNL")]
        [Route("ping")]
        public IActionResult Ping()
        {
            //var claims = _jwtHandler.GetUserClaims(Request.Headers);
            //if (!claims)
            //{
            //    Unauthorized();
            //}

            return Ok("pong");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            List<Claim> claims = new List<Claim> { new Claim("example", "123")};
            var tokenString = _jwtHandler.CreateToken(claims);
            return Ok(tokenString);
        }
    }
}
