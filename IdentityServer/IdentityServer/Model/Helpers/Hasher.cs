﻿using Microsoft.Extensions.FileProviders.Physical;
using System.Text;
using System.Security.Cryptography;

namespace IdentityServer.Model.Helpers
{
    public static class Hasher
    {
        public static string GetHash(string value)
        {
            string result;
            using (var hasher = SHA256Managed.Create())
                result = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(value)));

            return result;
        }
    }
}
