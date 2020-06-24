using System;

namespace DKrOSS.CalcSubnet.Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (IDumpable subnetMask in SubnetMask.AllSubnetMasks(32,0))
            {
                Console.WriteLine(subnetMask.Dump());
            }


            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
