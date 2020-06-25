// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Collections.Generic;
using System.Text;

namespace DKrOSS.CalcSubnet
{
    public class SubnetInfo : IDumpable
    {
        public SubnetInfo(IpAddress ipAddress, SubnetMask subnetMask)
        {
            IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            SubnetMask = subnetMask ?? throw new ArgumentNullException(nameof(subnetMask));
            NetworkAddress = ipAddress & subnetMask;
            FirstAddress = NetworkAddress + 1;
            BroadcastAddress = NetworkAddress | ~subnetMask;
            LastAddress = BroadcastAddress - 1;
        }

        public IpAddress IpAddress { get; }
        public SubnetMask SubnetMask { get; }
        public IpAddress NetworkAddress { get; }
        public IpAddress FirstAddress { get; }
        public IpAddress LastAddress { get; }
        public IpAddress BroadcastAddress { get; }


        public static IReadOnlyList<IpAddress> IpAddressList(SubnetInfo subnetInfo)
        {
            return IpAddressList(subnetInfo.IpAddress, subnetInfo.SubnetMask);
        }

        public static IReadOnlyList<IpAddress> IpAddressList(IpAddress ipAddress, SubnetMask subnetMask)
        {
            var addresses = new List<IpAddress>();
            var subnet = new SubnetInfo(ipAddress, subnetMask);

            var currentIpAddress = subnet.NetworkAddress;

            while (true)
            {
                var isNetworkPrefix = false;
                var isBroadcastAddress = false;

                if (currentIpAddress.Value == subnet.NetworkAddress.Value)
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
            sb.AppendLine($"IP address: {IpAddress}");
            sb.AppendLine($"Subnet mask: {SubnetMask}");
            sb.AppendLine($"Network address: {NetworkAddress}");
            sb.AppendLine($"First address {FirstAddress}");
            sb.AppendLine($"Last address: {LastAddress}");
            sb.AppendLine($"Broadcast address: {BroadcastAddress}");
            return sb.ToString();
        }
    }
}