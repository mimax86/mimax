using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Mimax.Cryptography
{
    public class Encryptor
    {
        private readonly RsaEncryption _rsa;
        private readonly AesEncryption _aes;
        private readonly DigitalSignature _digitalSignature;

        public Encryptor(RSACryptoServiceProvider cryptoServiceProvider)
        {
            _rsa = new RsaEncryption(cryptoServiceProvider);
            _aes = new AesEncryption();
            _digitalSignature = new DigitalSignature(cryptoServiceProvider);
        }

        public EncryptedPacket Encrypt(byte[] data)
        {
            var sessionKey = _aes.GenerateRandomNumber(32);
            var packet = new EncryptedPacket();
            packet.Iv = _aes.GenerateRandomNumber(16);
            packet.EncryptedData = _aes.Encrypt(data, sessionKey, packet.Iv);
            packet.EncryptedSessionKey = _rsa.Encrypt(sessionKey);
            using (var hmac = new HMACSHA256(sessionKey))
            {
                packet.Hmac = hmac.ComputeHash(Combine(packet.EncryptedData, packet.Iv));
            }
            packet.Signature = _digitalSignature.SignData(packet.Hmac);
            return packet;
        }

        public byte[] Decrypt(EncryptedPacket packet)
        {
            var sessionKey = _rsa.Decrypt(packet.EncryptedSessionKey);
            using (var hmac = new HMACSHA256(sessionKey))
            {
                var hmacToCheck = hmac.ComputeHash(Combine(packet.EncryptedData, packet.Iv));
                if (!Compare(packet.Hmac, hmacToCheck))
                    throw new CryptographicException("HMAC does not match encrypted packet.");
            }

            if (!_digitalSignature.Verify(packet.Hmac, packet.Signature))
                throw new CryptographicException("Digital signature cannot be verified.");
            return _aes.Decrypt(packet.EncryptedData, sessionKey, packet.Iv);
        }

        private bool Compare(byte[] first, byte[] second)
        {
            var result = first.Length == second.Length;
            for (int i = 0; i < first.Length && i < second.Length; i++)
            {
                result &= first[i] == second[i];
            }

            return result;
        }

        private byte[] Combine([NotNull] byte[] first, [NotNull] byte[] second)
        {
            var result = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            return result;
        }
    }
}