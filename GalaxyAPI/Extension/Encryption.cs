using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyAPI.Extension
{
    internal static class Encryption
    {
        public static string SHA(this string text)
        {
            using (var sha = SHA256.Create())
            {
                byte[] passwordByte = Encoding.UTF8.GetBytes(text);
                byte[] hashValue = sha.ComputeHash(passwordByte);
                return Convert.ToBase64String(hashValue);
            }
        }
    }
}
