using System.Security.Cryptography;
using System.Text;

namespace CacauShow.API.Cryptograph
{
    public static class Cryptograph
    {
        private static byte[] chave = { };
        private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };
        private static string chaveCriptografia = "!mais@pick";

        #region DES - Encrypt / Decrypt
        public static string Encrypt(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();
                input = Encoding.UTF8.GetBytes(valor); chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(string valor)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = new byte[valor.Length];
                input = Convert.FromBase64String(valor.Replace(" ", "+"));

                chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        #endregion

        #region SHA512
        public static string GetSHA512FromPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(string.Concat(password));

            using (SHA512 shaM = new SHA512Managed())
            {
                var hashedInputBytes = shaM.ComputeHash(data);

                return GetString(hashedInputBytes);
            }
        }
        private static string GetString(byte[] hashedInputBytes)
        {
            var hashedInputStringBuilder = new StringBuilder();

            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }

            return hashedInputStringBuilder.ToString();
        }
        #endregion

        #region Base64

        public static string EncryptBase64WithSalt(string message, string salt = "")
        {
            var bytesMessage = Encoding.UTF8.GetBytes(message);
            var bytesSalt = Encoding.UTF8.GetBytes(salt);
            var encrypt = Convert.ToBase64String(bytesMessage) + Convert.ToBase64String(bytesSalt);

            return encrypt;
        }

        public static string DecryptBase64WithSalt(string base64String, string salt = "")
        {
            var bytesSalt = Encoding.UTF8.GetBytes(salt);
            var base64Salt = Convert.ToBase64String(bytesSalt);

            var base64Text = base64String.Substring(0, (base64String.Length - base64Salt.Length));
            var message = Convert.FromBase64String(base64Text);

            var revertedString = Encoding.UTF8.GetString(message);

            return revertedString;
        }
        #endregion
 
    }
}
