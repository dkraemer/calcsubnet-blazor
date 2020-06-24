using System;

namespace DKrOSS.CalcSubnet.Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //foreach (IDumpable subnetMask in SubnetMask.AllSubnetMasks(32,0))
            //{
            //    Console.WriteLine(subnetMask.Dump());
            //}

            //DotDecimalUtils.TryParse("172.17.9.9", out var parsedValue);
            //var ipAddress = new IpAddress(parsedValue ?? throw new ArgumentNullException());
            //var subnetMask = new SubnetMask(28);
            //var subnet = new Subnet(ipAddress, subnetMask);

            //Console.WriteLine(subnet.Dump());

            //foreach (var address in Subnet.IpAddressList(ipAddress, subnetMask))
            //{
            //    Console.WriteLine(address.Dump());
            //}

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}