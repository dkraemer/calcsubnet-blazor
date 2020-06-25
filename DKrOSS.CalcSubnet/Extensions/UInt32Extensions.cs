// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;

namespace DKrOSS.CalcSubnet.Extensions
{
    public static class UInt32Extensions
    {
        public static string ToDotDecimalString(this uint value)
        {
            var octets = BitConverter.GetBytes(value);
            return $"{octets[3]}.{octets[2]}.{octets[1]}.{octets[0]}";
        }
    }
}