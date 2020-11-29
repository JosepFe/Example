using JosepApp.Configuration.JWT.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace JosepApp.Business.AuthManagement.Controller
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
        [Authorize]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            List<Claim> claims = new List<Claim>();
            var tokenString = _jwtHandler.CreateToken(claims);
            return Ok(tokenString);
        }
    }
}
