namespace DKrOSS.CalcSubnet
{
    public abstract class DotDecimal : IDumpable
    {
        public const byte ValueBitCount = 32;

        private string _dotDecimalString;
        private uint _value;

        public uint Value
        {
            get => _value;
            protected set
            {
                _value = value;
                _dotDecimalString = value.ToDotDecimalString();
            }
        }

        public override string ToString()
        {
            return _dotDecimalString;
        }

        public virtual string Dump()
        {
            return $"Value: {_dotDecimalString}";
        }
    }
}