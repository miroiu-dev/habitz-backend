using System.Security.Claims;

namespace Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(userId, out int parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }
}
