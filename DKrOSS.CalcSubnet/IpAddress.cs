namespace DKrOSS.CalcSubnet
{
    public class IpAddress : DotDecimal
    {
        public IpAddress(uint value, bool isNetworkPrefix = false, bool isBroadcastAddress = false)
        {
            Value = value;
            IsNetworkPrefix = isNetworkPrefix;
            IsBroadcastAddress = isBroadcastAddress;
        }

        public bool IsNetworkPrefix { get; }
        public bool IsBroadcastAddress { get; }

        public static implicit operator IpAddress(uint value)
        {
            return new IpAddress(value);
        }

        public override string Dump()
        {
            var suffix = "";
            if (IsNetworkPrefix)
            {
                suffix = "(Network prefix)";
            }
            else if (IsBroadcastAddress)
            {
                suffix = "(Broadcast address)";
            }

            return $"IP address: {this} {suffix}";
        }
    }
}