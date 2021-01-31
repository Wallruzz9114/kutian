using System;
using System.Security.Cryptography;

namespace Kutian.Utilities.Core.Utils
{
    public static class SecretGenerator
    {
        public static string GenerateSecret()
        {
            var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.GenerateKey();

            return Convert.ToBase64String(tripleDESCryptoServiceProvider.Key);
        }
    }
}