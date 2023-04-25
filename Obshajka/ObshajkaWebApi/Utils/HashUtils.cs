using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ObshajkaWebApi.Utils
{
    static class HashUtils
    {
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
