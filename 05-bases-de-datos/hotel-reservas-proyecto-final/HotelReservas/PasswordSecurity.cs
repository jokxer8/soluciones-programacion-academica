using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelReservas
{
    public static class PasswordSecurity
    {
        public const int MinimumIterations = 600000;
        public const int MaximumIterations = 2000000;
        private const string Prefix = "pbkdf2-sha256$v1$";

        public static int Iterations
        {
            get
            {
                string configured = Environment.GetEnvironmentVariable("HOTELRESERVAS_PASSWORD_ITERATIONS");
                if (string.IsNullOrEmpty(configured)) return MinimumIterations;
                int value;
                if (!int.TryParse(configured, NumberStyles.None, CultureInfo.InvariantCulture, out value)
                    || value < MinimumIterations || value > MaximumIterations)
                    throw new InvalidOperationException("HOTELRESERVAS_PASSWORD_ITERATIONS debe estar entre 600000 y 2000000.");
                return value;
            }
        }

        public static string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Debe escribir la clave.");
            int iterations = Iterations;
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return Prefix + iterations.ToString(CultureInfo.InvariantCulture) + "$"
                    + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(kdf.GetBytes(32));
        }

        public static bool IsLegacy(string stored)
        {
            return stored != null && Regex.IsMatch(stored, "\\A[0-9a-fA-F]{64}\\z");
        }

        public static bool Verify(string password, string stored, out bool needsUpgrade)
        {
            needsUpgrade = false;
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(stored)) return false;
            if (IsLegacy(stored))
            {
                byte[] expected = new byte[32];
                for (int i = 0; i < expected.Length; i++)
                    expected[i] = byte.Parse(stored.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                using (var sha = SHA256.Create())
                {
                    bool valid = Equal(expected, sha.ComputeHash(Encoding.UTF8.GetBytes(password.Trim())));
                    needsUpgrade = valid;
                    return valid;
                }
            }
            if (!stored.StartsWith(Prefix, StringComparison.Ordinal)) return false;
            string[] fields = stored.Split('$');
            int iterations;
            if (fields.Length != 5 || !int.TryParse(fields[2], NumberStyles.None, CultureInfo.InvariantCulture, out iterations)
                || iterations < MinimumIterations || iterations > MaximumIterations) return false;
            byte[] salt, key;
            try { salt = Convert.FromBase64String(fields[3]); key = Convert.FromBase64String(fields[4]); }
            catch (FormatException) { return false; }
            if (salt.Length != 16 || key.Length != 32
                || Convert.ToBase64String(salt) != fields[3] || Convert.ToBase64String(key) != fields[4]) return false;
            using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                bool valid = Equal(key, kdf.GetBytes(32));
                needsUpgrade = valid && iterations < Iterations;
                return valid;
            }
        }

        private static bool Equal(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int difference = 0;
            for (int i = 0; i < a.Length; i++) difference |= a[i] ^ b[i];
            return difference == 0;
        }
    }
}
