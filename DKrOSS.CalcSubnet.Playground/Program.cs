using System;

namespace DKrOSS.CalcSubnet.Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (IDumpable subnetMask in SubnetMask.AllSubnetMasks())
            {
                Console.WriteLine(subnetMask.Dump());
            }


            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
