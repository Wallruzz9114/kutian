using System;

namespace Kutian.Utilities.Core.Models
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}