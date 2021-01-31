namespace Kutian.Utilities.Abstractions
{
    public interface IPasswordHasher
    {
        string HashPassword(byte[] salt, string password);
    }
}