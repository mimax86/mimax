namespace Mimax.Cryptography
{
    public struct Hash
    {
        public byte[] Value { get; }

        public Hash(byte[] value)
        {
            Value = value;
        }

        public static implicit operator byte[](Hash hash) => hash.Value;
        public static implicit operator Hash(byte[] value) => new Hash(value);
    }
}