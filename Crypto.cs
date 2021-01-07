using System;
using System.Security.Cryptography;
using System.Text;

namespace VirturlMeetingAssitant.Backend
{
    public class KeyGenerator
    {
        // The characters we can generate a key from.
        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int size)
        {
            // Generating every random byte.
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                // Convert every 4 bytes into Unsigned Int.
                var rnd = BitConverter.ToUInt32(data, i * 4);
                // Pick an char from array randomly.
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}