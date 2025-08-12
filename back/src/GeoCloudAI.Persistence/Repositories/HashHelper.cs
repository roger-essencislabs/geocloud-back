using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeoCloudAI.Application.Helpers
{
    public class HashHelper
    {
        private byte[] ComputeSHA256Hash(string? input)
        {
            if (input != null)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Convert the input string to a byte array and compute the hash
                    return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                }
            }
            else
                return Array.Empty<byte>();
        }
        public string GetHashString(string? input)
        {
            if (input != null)
            {
                byte[] hash = ComputeSHA256Hash(input);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            else
                return string.Empty;
        }
    }
}
