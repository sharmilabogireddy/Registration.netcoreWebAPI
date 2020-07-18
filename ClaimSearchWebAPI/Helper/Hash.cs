using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSearchWebAPI.Helper
{
    public class Hash
    {
        public static string CreateHash(string Password, string Salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                         password: Password,
                         salt: Encoding.UTF8.GetBytes(Salt),
                         prf: KeyDerivationPrf.HMACSHA512,
                         iterationCount: 10000,
                         numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool VerifyHash(string Password, string Salt, string HashedPassword)
        {
            return CreateHash(Password, Salt) == HashedPassword;
        }
    }
}
