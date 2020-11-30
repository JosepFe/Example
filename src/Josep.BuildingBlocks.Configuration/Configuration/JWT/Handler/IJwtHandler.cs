using System.Collections.Generic;
using System.Security.Claims;

namespace JosepApp.BuildingBlocks.Configuration.Configuration.JWT.Handler
{
    public interface IJwtHandler
    {
        string CreateToken(List<Claim> clientClaims);
        List<Claim> GetUserClaims(string jwtToken);
    }
}
