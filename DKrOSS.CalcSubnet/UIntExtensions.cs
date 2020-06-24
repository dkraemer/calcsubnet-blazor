using System;

namespace DKrOSS.CalcSubnet
{
    public static class UIntExtensions
    {
        public static string ToDotDecimalString(this uint value)
        {
            var octets = BitConverter.GetBytes(value);
            return $"{octets[3]}.{octets[2]}.{octets[1]}.{octets[0]}";
        }
    }
}