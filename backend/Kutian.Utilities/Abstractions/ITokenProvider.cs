using System.Collections.Generic;
using System.Security.Claims;

namespace Kutian.Utilities.Abstractions
{
    public interface ITokenProvider
    {
        string Get(string uniqueName, List<Claim> customClaims = null);
    }
}