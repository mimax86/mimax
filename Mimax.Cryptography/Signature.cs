namespace Mimax.Cryptography
{
    public struct Signature
    {
        public byte[] Value { get; }

        public Signature(byte[] value)
        {
            Value = value;
        }

        public static implicit operator byte[](Signature hash) => hash.Value;
        public static implicit operator Signature(byte[] value) => new Signature(value);
    }
}