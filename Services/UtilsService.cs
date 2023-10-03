using System.Security.Cryptography;
using System.Text;

namespace wotNext.Services
{
    public class UtilsService
    {
        public static string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512.ComputeHash(passwordBytes);

                StringBuilder sb = new StringBuilder();
                foreach (var pByte in hashBytes)
                {
                    sb.Append(pByte.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}