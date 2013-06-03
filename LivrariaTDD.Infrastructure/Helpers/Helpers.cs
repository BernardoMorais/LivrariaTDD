using System;
using System.Security.Cryptography;

namespace LivrariaTDD.Infrastructure.Helpers
{
    public static class Helpers
    {
        public static string ConvertoToSHA1(string text)
        {
            try
            {
                var encoder = new System.Text.ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(text);
                var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(
                    cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

                return hash.ToLower();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
