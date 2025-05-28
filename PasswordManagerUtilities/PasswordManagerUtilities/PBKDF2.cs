using System.Text;
using System.Security.Cryptography;

namespace PasswordManagerUtilities
{
    public class PBKDF2
    {

        // Static methods that utilise method overloading to accomodate for different data types of passwords and salts.
        public static byte[] DeriveKey(string password, string salt, int iterations)
        {
            byte[] rawPassword = UTF8Encoding.UTF8.GetBytes(password);
            byte[] rawSalt = UTF8Encoding.UTF8.GetBytes(salt);
            
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(rawPassword, rawSalt, iterations, HashAlgorithmName.SHA256);

            return key.GetBytes(32);
        }

        public static byte[] DeriveKey(byte[] password, string salt, int iterations)
        {
            byte[] rawSalt = UTF8Encoding.UTF8.GetBytes(salt);

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, rawSalt, iterations, HashAlgorithmName.SHA256);

            return key.GetBytes(32);
        }

        public static byte[] DeriveKey(byte[] password, byte[] salt, int iterations)
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);

            return key.GetBytes(32);
        }
    }
}
