using System.Security.Cryptography;
using System.Text;

namespace wizardsoft_testtask.Service.Auth
{
    public class AuthUtil
    {
        public static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
