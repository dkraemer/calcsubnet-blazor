using System;
using System.Collections.Generic;
using System.Text;

namespace DKrOSS.CalcSubnet
{
    public class Subnet : IDumpable
    {
        public Subnet(IpAddress ipAddress, SubnetMask subnetMask)
        {
            IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            SubnetMask = subnetMask ?? throw new ArgumentNullException(nameof(subnetMask));
            NetworkPrefix = ipAddress & subnetMask;
            FirstAddress = NetworkPrefix + 1;
            BroadcastAddress = NetworkPrefix | ~subnetMask;
            LastAddress = BroadcastAddress - 1;
        }

        public IpAddress IpAddress { get; }
        public SubnetMask SubnetMask { get; }
        public IpAddress NetworkPrefix { get; }
        public IpAddress FirstAddress { get; }
        public IpAddress BroadcastAddress { get; }
        public IpAddress LastAddress { get; }

        public static IReadOnlyList<IpAddress> IpAddressList(IpAddress ipAddress, SubnetMask subnetMask)
        {
            var addresses = new List<IpAddress>();
            var subnet = new Subnet(ipAddress, subnetMask);

            var currentIpAddress = subnet.NetworkPrefix;

            while (true)
            {
                var isNetworkPrefix = false;
                var isBroadcastAddress = false;

                if (currentIpAddress.Value == subnet.NetworkPrefix.Value)
                {
                    isNetworkPrefix = true;
                }
                else if (currentIpAddress.Value == subnet.BroadcastAddress.Value)
                {
                    isBroadcastAddress = true;
                }

                addresses.Add(new IpAddress(currentIpAddress, isNetworkPrefix, isBroadcastAddress));

                if (isBroadcastAddress)
                {
                    break;
                }

                currentIpAddress++;
            }

            return addresses;
        }

        public string Dump()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Ip address: {IpAddress}");
            sb.AppendLine($"Subnet mask: {SubnetMask}");
            sb.AppendLine($"Network prefix: {NetworkPrefix}");
            sb.AppendLine($"First address {FirstAddress}");
            sb.AppendLine($"Last address: {LastAddress}");
            sb.AppendLine($"Broadcast address: {BroadcastAddress}");
            return sb.ToString();
        }
    }
}