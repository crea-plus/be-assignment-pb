using PhoneBook.Application.Core;
using System.Security.Claims;

namespace PhoneBook.Application.Helpers
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirstValue(CustomClaims.UserId);

            return Guid.Parse(userId);
        }

        public static bool TryGetUserId(this ClaimsPrincipal principal, out Guid? userId)
        {
            var userIdStr = principal.FindFirstValue(CustomClaims.UserId);
            if (userIdStr == null)
            {
                userId = null;

                return false;
            }

            userId = Guid.Parse(userIdStr);

            return true;
        }

        public static bool TryGetUserData(this ClaimsPrincipal principal, out UserData? userData)
        {
            var userIdStr = principal.FindFirstValue(CustomClaims.UserId);
            var roleStr = principal.FindFirstValue(ClaimTypes.Role);
            if (userIdStr == null || roleStr == null)
            {
                userData = null;

                return false;
            }

            var userId = Guid.Parse(userIdStr);
            var role = Enum.Parse<RoleEnum>(roleStr);

            userData = new UserData(userId, role);

            return true;
        }
    }
}