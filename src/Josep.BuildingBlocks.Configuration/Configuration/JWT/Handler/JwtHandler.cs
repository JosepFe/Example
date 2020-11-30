using JosepApp.BuildingBlocks.Configuration.Common.Options.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JosepApp.BuildingBlocks.Configuration.Configuration.JWT.Handler
{
    public class JwtHandler : AuthorizationHandler<ValidatePostNLRequirement>, IJwtHandler
    {
        private JwtOptions JwtOptions { get; }
        private static SecurityKey SecurityKey { get; set; }

        public JwtHandler(JwtOptions jwtOptions)
        {
            JwtOptions = jwtOptions;
        }

        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            JwtOptions = jwtOptions.Value;
        }

        public string CreateToken(List<Claim> clientClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtOptions.Issuer,
                Audience = JwtOptions.Audience,
                Subject = new ClaimsIdentity(clientClaims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(JwtOptions.ClockSkew)),
                IssuedAt = DateTime.Now,
                Claims = clientClaims.Where(c => c.Type != ClaimTypes.Role).ToDictionary(x => x.Type, x => x.Value as object),
                SigningCredentials = credentials
            };
            return new JwtSecurityTokenHandler().CreateEncodedJwt(tokenDescriptor);
        }

        public List<Claim> GetUserClaims(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var claimsPrincipal = handler.ValidateToken(jwtToken,
                new TokenValidationParameters
                {
                    ValidAudience = JwtOptions.Audience,
                    ValidIssuer = JwtOptions.Issuer,
                    RequireSignedTokens = false,
                    TokenDecryptionKey = SecurityKey,
                    IssuerSigningKey = SecurityKey
                }, out _);

            return claimsPrincipal.Claims.ToList();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidatePostNLRequirement requirement)
        {
            // var claims = context.User.Claims.ToArray();

            if (!context.User.HasClaim(c => c.Issuer == "Josep" && c.Type == "DocumentTypes"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //if (!context.User.HasClaim(c => c.Issuer == "Josep" && c.Type == "Example"))
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //if (!int.TryParse(context.User.FindFirst(c => c.Issuer == "Josep" && c.Type == "Example").Value, out int value))
            //{
            //    context.Succeed(requirement);
            //}

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
