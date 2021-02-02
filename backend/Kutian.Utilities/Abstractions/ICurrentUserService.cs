using System.Security.Claims;

namespace Kutian.Utilities.Abstractions
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal GetClaimsPrincipal();
    }
}