using System.Security.Cryptography;
using System.Text;

namespace Web.Helpers
{
    public static class Password
    {
        public static Byte[] GenerarHash(string passsword)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passsword));
                return bytes;
            }
        }

        public static string ObtenerHash(byte[] hash)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
