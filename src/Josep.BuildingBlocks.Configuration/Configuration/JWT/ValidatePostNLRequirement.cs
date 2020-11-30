using Microsoft.AspNetCore.Authorization;

namespace JosepApp.BuildingBlocks.Configuration.Configuration.JWT
{
    public class ValidatePostNLRequirement : IAuthorizationRequirement
    {
        public ValidatePostNLRequirement(int value)
        {
            Example = value;
        }

        public int Example { get; private set; }
    }
}
