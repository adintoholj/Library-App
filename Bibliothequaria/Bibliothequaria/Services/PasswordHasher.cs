using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ===== REGISTER / LOGIN (START) =====
using System.Security.Cryptography;

namespace Bibliothequaria.Services
{
    public static class PasswordHasher
    {
        public const int DefaultIterations = 120_000;   // matches the column default
        public const int SaltSize = 32;                 // 256-bit
        public const int HashSize = 32;                 // 256-bit

        public static (byte[] hash, byte[] salt, int iterations) CreateHash(string password, int? iterations = null)
        {
            var iters = iterations ?? DefaultIterations;
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iters, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);
            return (hash, salt, iters);
        }

        public static bool Verify(string password, byte[] hash, byte[] salt, int iterations)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var candidate = pbkdf2.GetBytes(HashSize);
            return CryptographicOperations.FixedTimeEquals(candidate, hash);
        }
    }
}
// ===== REGISTER / LOGIN (END) =====

