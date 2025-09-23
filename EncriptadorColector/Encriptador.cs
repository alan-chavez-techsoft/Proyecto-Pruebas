using System.Security.Cryptography;
using System.Text;

namespace EncriptadorColector
{
    public static class Encriptador
    {
        // Constantes
        public const int AesBlockSizeBytes = 16;              // 128 bits (bloque fijo de AES)
        public const int AesKeySizeBytes = 32;              // 256 bits
        public const int HmacKeySizeBytes = 32;              // 256 bits
        public const int HmacTagSizeBytes = 32;              // SHA-256 -> 32 bytes

        /// <summary>
        /// Cifra un mensaje en AES-CBC con PKCS7 y autentica con HMAC-SHA256 (Encrypt-then-MAC).
        /// Devuelve Base64( IV || CIPHERTEXT || TAG )
        /// </summary>
        public static string CifrarCbcHmac(string accesoSimetricoBase64, string codigoAutentificacionHashBase64, string mensaje)
        {
            if (mensaje is null) throw new ArgumentNullException(nameof(mensaje));
            var aesKey = Convert.FromBase64String(accesoSimetricoBase64);
            var hmacKey = Convert.FromBase64String(codigoAutentificacionHashBase64);

            ValidarLongitudesClaves(aesKey, hmacKey);

            // IV aleatorio de 16 bytes
            var iv = GenerarInitializationVector();

            byte[] cipherText;
            using (var aes = Aes.Create())
            {
                aes.KeySize = AesKeySizeBytes * 8;
                aes.BlockSize = AesBlockSizeBytes * 8;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor(aesKey, iv);
                var plain = Encoding.UTF8.GetBytes(mensaje);
                cipherText = Transform(encryptor, plain);
            }

            // Construimos (IV || CIPHERTEXT) y generamos TAG HMAC
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(iv);
                bw.Write(cipherText);
            }

            var dataToMac = ms.ToArray();
            byte[] tag;
            using (var hmac = new HMACSHA256(hmacKey))
                tag = hmac.ComputeHash(dataToMac);

            // Empaquetar: IV || CIPHERTEXT || TAG
            using var final = new MemoryStream();
            using (var bw = new BinaryWriter(final, Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(dataToMac);
                bw.Write(tag);
            }
            return Convert.ToBase64String(final.ToArray());
        }

        /// <summary>
        /// Descifra un paquete Base64( IV || CIPHERTEXT || TAG ) y valida el HMAC.
        /// </summary>
        public static string DescifrarCbcHmac(string accesoSimetricoBase64, string codigoAutentificacionHashBase64, string paqueteBase64)
        {
            var aesKey = Convert.FromBase64String(accesoSimetricoBase64);
            var hmacKey = Convert.FromBase64String(codigoAutentificacionHashBase64);
            var package = Convert.FromBase64String(paqueteBase64);

            ValidarLongitudesClaves(aesKey, hmacKey);
            if (package.Length < AesBlockSizeBytes + HmacTagSizeBytes)
                throw new CryptographicException("Paquete inválido: longitud insuficiente.");

            // Particionar: [ IV | CIPHERTEXT | TAG ]
            var iv = new byte[AesBlockSizeBytes];
            Buffer.BlockCopy(package, 0, iv, 0, iv.Length);

            var tag = new byte[HmacTagSizeBytes];
            Buffer.BlockCopy(package, package.Length - tag.Length, tag, 0, tag.Length);

            var cipherLen = package.Length - iv.Length - tag.Length;
            if (cipherLen <= 0) throw new CryptographicException("Paquete inválido: sin texto cifrado.");

            var cipherText = new byte[cipherLen];
            Buffer.BlockCopy(package, iv.Length, cipherText, 0, cipherLen);

            // Recalcular HMAC sobre (IV || CIPHERTEXT)
            byte[] expectedTag;
            using (var hmac = new HMACSHA256(hmacKey))
            {
                using var ms = new MemoryStream();
                using (var bw = new BinaryWriter(ms, Encoding.UTF8, leaveOpen: true))
                {
                    bw.Write(iv);
                    bw.Write(cipherText);
                }
                expectedTag = hmac.ComputeHash(ms.ToArray());
            }

            if (!ConstantTimeEquals(tag, expectedTag))
                throw new CryptographicException("Autenticación fallida (HMAC inválido).");

            // Descifrar
            using var aes = Aes.Create();
            aes.KeySize = AesKeySizeBytes * 8;
            aes.BlockSize = AesBlockSizeBytes * 8;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aesKey, iv);
            var plainBytes = Transform(decryptor, cipherText);
            return Encoding.UTF8.GetString(plainBytes);
        }

        // === Utilidades privadas ===

        private static void ValidarLongitudesClaves(byte[] aesKey, byte[] hmacKey)
        {
            if (aesKey is null || aesKey.Length != AesKeySizeBytes)
                throw new CryptographicException($"La clave AES debe tener {AesKeySizeBytes} bytes (Base64 de 256 bits).");
            if (hmacKey is null || hmacKey.Length != HmacKeySizeBytes)
                throw new CryptographicException($"La clave HMAC debe tener {HmacKeySizeBytes} bytes (Base64 de 256 bits).");
        }

        private static byte[] Transform(ICryptoTransform transform, byte[] input)
        {
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
            {
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();
            }
            return ms.ToArray();
        }

        private static bool ConstantTimeEquals(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        public static byte[] GenerarInitializationVector()
        {
            var iv = new byte[AesBlockSizeBytes];
            RandomNumberGenerator.Fill(iv);
            return iv;
        }
    }
}
