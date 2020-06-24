namespace DKrOSS.CalcSubnet
{
    public abstract class DotDecimal : IDumpable
    {
        public const byte ValueBitCount = 32;
        public uint Value { get; protected set; }

        public override string ToString()
        {
            return Value.ToDotDecimalString();
        }

        public static implicit operator uint(DotDecimal obj)
        {
            return obj.Value;
        }

        public abstract string Dump();
    }
}