// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using DKrOSS.CalcSubnet.Extensions;

namespace DKrOSS.CalcSubnet.Playground
{
    internal class Program
    {
        private static void DumpSubnets(byte startPrefixLength, byte endPrefixLength)
        {
            foreach (IDumpable subnetMask in SubnetMask.GetAll(startPrefixLength, endPrefixLength))
            {
                Console.WriteLine(subnetMask.Dump());
            }
        }

        private static void DumpIpAddressList(string ipAddressString, byte prefixLength)
        {
            if (!ipAddressString.TryParseDotDecimal(out var parsedIpAddress))
            {
                throw new Exception();
            }

            var ipAddress = new IpAddress(parsedIpAddress);
            var subnetMask = SubnetMask.Create(prefixLength);
            var subnetInfo = new SubnetInfo(ipAddress, subnetMask);

            Console.WriteLine(subnetInfo.Dump());

            foreach (var address in SubnetInfo.IpAddressList(subnetInfo))
            {
                Console.WriteLine(address.Dump());
            }
        }


        private static void Main(string[] args)
        {
            
            //DumpSubnets(32,0);
            DumpIpAddressList("131.220.16.221", 30);


            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}