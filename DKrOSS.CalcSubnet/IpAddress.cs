// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

namespace DKrOSS.CalcSubnet
{
    public class IpAddress : DotDecimal
    {
        public IpAddress(uint? ipAddress, bool isNetworkPrefix = false, bool isBroadcastAddress = false)
            : base(ipAddress)
        {
            IsNetworkPrefix = isNetworkPrefix;
            IsBroadcastAddress = isBroadcastAddress;
        }

        public bool IsNetworkPrefix { get; }
        public bool IsBroadcastAddress { get; }

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

        public static implicit operator IpAddress(uint? value)
        {
            return new IpAddress(value);
        }

        public static implicit operator uint?(IpAddress obj)
        {
            return obj.Value;
        }
    }
}