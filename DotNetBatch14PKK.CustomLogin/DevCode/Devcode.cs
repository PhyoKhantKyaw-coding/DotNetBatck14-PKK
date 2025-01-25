using System.Security.Cryptography;
using System.Text;

namespace DotNetBatch14PKK.CustomLogin.DevCode
{

    public static class DevCode
    {
        public static string ToHashPassword(this string password, string userName, string secretKey)
        {
            // Create a SHA256 hash of the password
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(
                password +
                userName.Replace("a", "@").Replace("l", "!") +
                secretKey));

            // Convert the byte array to a hexadecimal string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

}
