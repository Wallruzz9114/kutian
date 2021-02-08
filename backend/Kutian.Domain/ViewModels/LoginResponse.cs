using System;

namespace Kutian.Domain.ViewModels
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}