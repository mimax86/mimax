using System;
using System.Security.Cryptography;
using System.Text;

namespace Mimax.Cryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            var encryptor = new Encryptor(new RSACryptoServiceProvider(2048));

            var document = Encoding.UTF8.GetBytes("Very important document");

            var packet = encryptor.Encrypt(document);

            var decryptedDocument = encryptor.Decrypt(packet);

            Console.WriteLine(Encoding.UTF8.GetString(decryptedDocument));
        }
    }
}