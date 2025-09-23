using System.Security.Cryptography;
using System.Text;

namespace EncriptadorColector
{
    public class Encriptador2
    {
        private readonly int IV_SIZE = 16;          // 128 bits, bloque de AES
        private readonly int HMAC_SIZE = 32;        // 256 bits (SHA256)

        // Cifra el mensaje con AES-CBC + HMACSHA256
        public string CifradoCbc(string accesoSimetrico, string codigoAutentificacionHash, string mensaje)
        {
            try
            {
                byte[] cipherText;
                byte[] iv = GenerarInitializationVector();

                var accesoSimetricoBytes = Convert.FromBase64String(accesoSimetrico);
                var codigoAutentificacionHashBytes = Convert.FromBase64String(codigoAutentificacionHash);
                var valorBytes = Encoding.UTF8.GetBytes(mensaje);

                using (var aes = new AesManaged
                {
                    KeySize = accesoSimetricoBytes.Length * 8,  // soporta AES-128,192,256
                    BlockSize = IV_SIZE * 8,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    using (var encrypter = aes.CreateEncryptor(accesoSimetricoBytes, iv))
                    using (var cipherStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(valorBytes, 0, valorBytes.Length);
                            cryptoStream.FlushFinalBlock();
                        }
                        cipherText = cipherStream.ToArray();
                    }
                }

                using (var encryptedStream = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(encryptedStream))
                    {
                        // Concatenamos IV + CIPHERTEXT
                        binaryWriter.Write(iv);
                        binaryWriter.Write(cipherText);
                        binaryWriter.Flush();

                        // Calculamos HMAC sobre (IV || CIPHERTEXT)
                        using (var hmac = new HMACSHA256(codigoAutentificacionHashBytes))
                        {
                            var tag = hmac.ComputeHash(encryptedStream.ToArray());
                            binaryWriter.Write(tag);
                        }
                    }
                    return Convert.ToBase64String(encryptedStream.ToArray());
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        // Descifra el mensaje, valida integridad con HMAC
        public string DescifradoCbc(string accesoSimetrico, string codigoAutentificacionHash, string mensajeCifrado)
        {
            try
            {
                var accesoSimetricoBytes = Convert.FromBase64String(accesoSimetrico);
                var codigoAutentificacionHashBytes = Convert.FromBase64String(codigoAutentificacionHash);
                var paquete = Convert.FromBase64String(mensajeCifrado);

                if (paquete.Length < IV_SIZE + HMAC_SIZE)
                    throw new CryptographicException("Mensaje inválido o corrupto.");

                // Extraer IV
                var iv = new byte[IV_SIZE];
                Buffer.BlockCopy(paquete, 0, iv, 0, IV_SIZE);

                // Extraer TAG
                var tag = new byte[HMAC_SIZE];
                Buffer.BlockCopy(paquete, paquete.Length - HMAC_SIZE, tag, 0, HMAC_SIZE);

                // Extraer CIPHERTEXT
                var cipherTextLength = paquete.Length - IV_SIZE - HMAC_SIZE;
                var cipherText = new byte[cipherTextLength];
                Buffer.BlockCopy(paquete, IV_SIZE, cipherText, 0, cipherTextLength);

                // Recalcular HMAC sobre (IV || CIPHERTEXT)
                byte[] expectedTag;
                using (var hmac = new HMACSHA256(codigoAutentificacionHashBytes))
                {
                    byte[] dataToMac = new byte[IV_SIZE + cipherTextLength];
                    Buffer.BlockCopy(iv, 0, dataToMac, 0, IV_SIZE);
                    Buffer.BlockCopy(cipherText, 0, dataToMac, IV_SIZE, cipherTextLength);

                    expectedTag = hmac.ComputeHash(dataToMac);
                }

                // Comparar TAG recibido vs calculado
                if (!ConstantTimeEquals(tag, expectedTag))
                    throw new CryptographicException("HMAC inválido, integridad comprometida.");

                // Descifrar
                using (var aes = new AesManaged
                {
                    KeySize = accesoSimetricoBytes.Length * 8,
                    BlockSize = IV_SIZE * 8,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    using (var decrypter = aes.CreateDecryptor(accesoSimetricoBytes, iv))
                    using (var cipherStream = new MemoryStream(cipherText))
                    using (var cryptoStream = new CryptoStream(cipherStream, decrypter, CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cryptoStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        // Genera un IV aleatorio de 16 bytes
        public byte[] GenerarInitializationVector()
        {
            byte[] IV_KEY = new byte[IV_SIZE];
            RandomNumberGenerator.Fill(IV_KEY);
            return IV_KEY;
        }

        // Comparación en tiempo constante para el HMAC
        private bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}