using System.Text;
using System.Security.Cryptography;

namespace ObshajkaWebApi.Utils
{
    internal static class HashUtils
    {
        /// <summary>
        /// Хеширует введенную строку.
        /// </summary>
        /// <param name="str">Строка, которую нужно захешировать.</param>
        /// <returns></returns>
        public static string GetHashString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var hash = SHA256.Create())
            {
                var hashedBytes = hash.ComputeHash(bytes);
                return Encoding.ASCII.GetString(hashedBytes);
            }
        }
    }
}
