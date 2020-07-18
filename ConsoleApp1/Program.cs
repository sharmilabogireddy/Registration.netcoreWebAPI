using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = "admin";
            var salt = "This is my Sea Salt and Brown Sugar";
            var valueBytes = KeyDerivation.Pbkdf2(
                         password: value,
                         salt: Encoding.UTF8.GetBytes(salt),
                         prf: KeyDerivationPrf.HMACSHA512,
                         iterationCount: 10000,
                         numBytesRequested: 256 / 8);

            Console.WriteLine(Convert.ToBase64String(valueBytes));
        }
    }
}
