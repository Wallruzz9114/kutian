using System.Security.Claims;
using Kutian.Utilities.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Kutian.Utilities.Core.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public ClaimsPrincipal GetClaimsPrincipal() => _httpContextAccessor.HttpContext.User;
    }
}