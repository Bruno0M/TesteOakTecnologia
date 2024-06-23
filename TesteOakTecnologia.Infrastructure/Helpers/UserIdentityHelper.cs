using System.Security.Claims;

namespace TesteOakTecnologia.Infrastructure.Helpers
{
    public class UserIdentityHelper
    {
        public static int GetCurrentUserId(ClaimsPrincipal User)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.Parse(userIdClaim);
        }
    }
}
