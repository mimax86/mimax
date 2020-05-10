using System.Security.Cryptography;

namespace Mimax.Cryptography
{
    public class DigitalSignature
    {
        private readonly RSACryptoServiceProvider _cryptoServiceProvider;
        private readonly RSAParameters _publicKey;
        private readonly RSAParameters _privateKey;

        public DigitalSignature(RSACryptoServiceProvider cryptoServiceProvider)
        {
            _cryptoServiceProvider = cryptoServiceProvider;
            _publicKey = cryptoServiceProvider.ExportParameters(false);
            _privateKey = cryptoServiceProvider.ExportParameters(true);
        }

        public Signature SignData(Hash hash)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                var formatter = new RSAPKCS1SignatureFormatter(rsa);
                formatter.SetHashAlgorithm("SHA256");
                return formatter.CreateSignature(hash);
            }
        }

        public bool Verify(Hash hash, Signature signature)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(_publicKey);
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                return deformatter.VerifySignature(hash, signature);
            }
        }
    }
}