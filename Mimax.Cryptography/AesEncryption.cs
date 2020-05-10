using System.IO;
using System.Security.Cryptography;

namespace Mimax.Cryptography
{
    public class AesEncryption
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                var random = new byte[length];
                generator.GetBytes(random);
                return random;
            }
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var stream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return stream.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var stream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return stream.ToArray();
                }
            }
        }
    }
}