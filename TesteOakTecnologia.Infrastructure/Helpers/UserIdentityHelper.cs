using System.Security.Claims;

namespace TesteOakTecnologia.Infrastructure.Helpers
{
    public class UserIdentityHelper
    {
        public static Guid GetCurrentUserId(ClaimsPrincipal User)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.Parse(userIdClaim);
        }
    }
}
