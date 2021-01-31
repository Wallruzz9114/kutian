using System;
using Kutian.Utilities.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Kutian.Utilities.Core.Utils
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(byte[] salt, string password) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, salt, prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
    }
}