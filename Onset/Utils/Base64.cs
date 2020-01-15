using System;
using System.Text;

namespace Onset.Utils
{
    /// <summary>
    /// Represents a helper class for Base64 encoding and decoding.
    /// </summary>
    public class Base64
    {
        public static string Encode(string raw)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(raw);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Decode(string encoded)
        {
            var base64EncodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
