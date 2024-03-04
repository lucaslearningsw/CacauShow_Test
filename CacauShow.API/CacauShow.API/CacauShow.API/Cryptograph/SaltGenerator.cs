using System.Security.Cryptography;
using System.Text;

namespace CacauShow.API.Cryptograph
{
    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            byte[] saltBytes = new byte[SALT_SIZE];

            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            string saltString = GetString(saltBytes);

            return saltString;
        }

        private static string GetString(byte[] hashedInputBytes)
        {
            var hashedInputStringBuilder = new StringBuilder();
            var count = 0;

            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("x2"));
                if (count % 6 == 0 && count > 0) { hashedInputStringBuilder.Append('-'); }
                count++;
            }

            return hashedInputStringBuilder.ToString();
        }
    }
}
