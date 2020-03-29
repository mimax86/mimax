using System;

namespace Mimax.Susquehanna
{
    public struct MachineValue : IEquatable<MachineValue>
    {
        public static MachineValue Invalid = new MachineValue(int.MinValue);
        public static MachineValue Max = new MachineValue((1 << 20) - 1);
        public static MachineValue Min = new MachineValue(0);

        public int Value { get; }

        private MachineValue(int value)
        {
            Value = value;
        }

        public static bool TryParse(string s, out MachineValue result)
        {
            if (!int.TryParse(s, out var value))
            {
                result = Invalid;
                return false;
            }

            if (value < Min.Value || value > Max.Value)
            {
                result = Invalid;
                return false;
            }

            result = new MachineValue(value);
            return true;
        }

        public MachineValue Add(MachineValue other)
        {
            var value = Value + other.Value;
            if (value < Min.Value || value > Max.Value)
                return Invalid;
            return new MachineValue(value);
        }

        public MachineValue Subtract(MachineValue other)
        {
            var value = Value - other.Value;
            if (value < Min.Value || value > Max.Value)
                return Invalid;
            return new MachineValue(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(MachineValue other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is MachineValue other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(MachineValue left, MachineValue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MachineValue left, MachineValue right)
        {
            return !left.Equals(right);
        }
    }
}