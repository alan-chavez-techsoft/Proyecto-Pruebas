using System.Security.Cryptography;
using System.Text;

namespace ProcesosFundacion.Tools.Utilerias
{
	public class Crypto
	{
        private static readonly int IV_SIZE = 16;
        private static readonly int KEY_SIZE = 256;

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        private static byte[] CopyOfRange(byte[] src, int start, int end)
        {
            int len = end - start;
            byte[] dest = new byte[len];
            Array.Copy(src, start, dest, 0, len);
            return dest;
        }

        public static byte[] GenerarHMAC(byte[] keyBytes, byte[] text)
        {
            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
            {
                hash.Initialize();
                var compute = hash.ComputeHash(text);
                return compute;
            }
        }


        public static string DecryptAes(string aesKeyBase64, string hmacKeyBase64, string valorCampo)
        {
            try
            {
                var aesKey = Convert.FromBase64String(aesKeyBase64);
                var hmacKey = Convert.FromBase64String(hmacKeyBase64);
                int macLength = hmacKey.Length;
                byte[] iv_cipherText_hmac = Convert.FromBase64String(valorCampo);
                int cipherTextLength = iv_cipherText_hmac.Length - macLength;

                byte[] iv = iv_cipherText_hmac.Take(IV_SIZE).ToArray();
                byte[] cipherText = CopyOfRange(iv_cipherText_hmac, IV_SIZE, cipherTextLength);
                byte[] iv_cipherText = Combine(iv, cipherText);
                byte[] receivedHMAC = CopyOfRange(iv_cipherText_hmac, cipherTextLength, iv_cipherText_hmac.Length);
                byte[] calculatedHMAC = GenerarHMAC(hmacKey, iv_cipherText);


                if (receivedHMAC.SequenceEqual(calculatedHMAC))
                {

                    using (RijndaelManaged aes = new RijndaelManaged())
                    {
                        aes.BlockSize = 128;
                        aes.KeySize = KEY_SIZE;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.Key = aesKey;
                        aes.IV = iv;

                        ICryptoTransform descifrador = aes.CreateDecryptor();
                        byte[] bytesDescifrado = descifrador.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        aes.Dispose();
                        var demo = Convert.ToBase64String(bytesDescifrado);
                        return Encoding.UTF8.GetString(bytesDescifrado);
                    }

                }
                else
                {
                    return "invalido";
                }
            }
            catch (InvalidOperationException) { }
            catch (ArgumentNullException) { }
            catch (ArgumentOutOfRangeException) { }
            catch (FormatException) { }
            catch (EncoderFallbackException) { }

            return "invalidotry";
        }
        public static string EncryptAes(string aesKeyBase64, string hmacKeyBase64, string valorCampo)
        {
            try
            {
                var aesKey = Convert.FromBase64String(aesKeyBase64);
                var hmacKey = Convert.FromBase64String(hmacKeyBase64);

                using (Rijndael aes = Rijndael.Create())
                {
                    aes.BlockSize = 128;
                    aes.KeySize = KEY_SIZE;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = aesKey;

                    ICryptoTransform descifrador = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[] plainText = Encoding.UTF8.GetBytes(valorCampo);
                    byte[] cipherText = descifrador.TransformFinalBlock(plainText, 0, plainText.Length);

                    byte[] iv_cipherText = Combine(aes.IV, cipherText);
                    byte[] hmac = GenerarHMAC(hmacKey, iv_cipherText);
                    byte[] iv_cipherText_hmac = Combine(iv_cipherText, hmac);
                    var iv_cipherText_hmac_base64 = Convert.ToBase64String(iv_cipherText_hmac);
                    aes.Dispose();
                    return iv_cipherText_hmac_base64;
                }
            }
            catch (Exception e) { }
            return string.Empty;
        }
    }
}
