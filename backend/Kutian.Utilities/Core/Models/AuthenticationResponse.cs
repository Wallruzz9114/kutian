namespace Kutian.Utilities.Core
{
    public class AuthenticationResponse
    {
        public string TokenPath { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string JWTKey { get; set; }
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public string AuthenticationType { get; set; }
    }
}