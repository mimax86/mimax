using System.Security.Cryptography;

namespace Mimax.Cryptography
{
    public class RsaEncryption
    {
        public RSAParameters PublicKey { get; private set; }
        public RSAParameters PrivateKey { get; private set; }

        public RsaEncryption(RSACryptoServiceProvider cryptoServiceProvider)
        {
            cryptoServiceProvider.PersistKeyInCsp = false;
            PublicKey = cryptoServiceProvider.ExportParameters(false);
            PrivateKey = cryptoServiceProvider.ExportParameters(true);
        }

        public byte[] Encrypt(byte[] data)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(PublicKey);
                return rsa.Encrypt(data, true);
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(PrivateKey);
                return rsa.Decrypt(data, true);
            }
        }
    }
}