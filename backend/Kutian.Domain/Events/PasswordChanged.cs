namespace Kutian.Domain.Events
{
    public class PasswordChanged
    {
        public PasswordChanged(string password) => Password = password;

        public string Password { get; }
    }
}