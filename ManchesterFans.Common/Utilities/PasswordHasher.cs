using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ManchesterFans.Common.Utilities
{
    public static class PasswordHasher
    {
        public static string ToSHA256(this string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();

            foreach (var i in bytes)
            {
                builder.Append(i.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
