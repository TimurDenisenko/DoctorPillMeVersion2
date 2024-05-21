using System.Security.Cryptography;
using System.Text;

namespace DoctorPillMe.Models
{
    [Obsolete]
    public static class PasswordSecurity
    {
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static Tuple<string, byte[]> HashPassword(string password, byte[]? salt = null)
        {
            salt = salt == null ? GenerateSalt() : salt;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] combined = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
                byte[] hashedBytes = sha256.ComputeHash(combined);
                return new Tuple<string, byte[]>(Convert.ToBase64String(hashedBytes), salt);
            }
        }
        public static bool VerifyPassword(string inputPassword, string storedHash, byte[] storedSalt) => 
            HashPassword(inputPassword, storedSalt).Item1 == storedHash;
    }
}
