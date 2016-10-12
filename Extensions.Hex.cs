using System;

namespace CSharpUtils
{
    public static partial class Extensions
    {
        private static readonly uint[] _lookup32 = BakeLookup32();

        private static uint[] BakeLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }

        private static string ToHexString(this byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        public static byte[] ToByteArray(this string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("Invalid Hex String");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexValue(hex[i << 1]) << 4) + (GetHexValue(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexValue(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
