// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Text.RegularExpressions;

namespace DKrOSS.CalcSubnet.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex DotDecimalRegex =
            new Regex(@"^(?<A>\d{1,3})\.(?<B>\d{1,3})\.(?<C>\d{1,3})\.(?<D>\d{1,3})$");

        public static bool TryParseDotDecimal(this string dotDecimalString, out uint? parsedValue)
        {
            parsedValue = null;
            if (dotDecimalString == null)
            {
                return false;
            }

            var match = DotDecimalRegex.Match(dotDecimalString.Trim());
            if (!match.Success)
            {
                return false;
            }

            if (!byte.TryParse(match.Groups["A"].Value, out var octetA))
            {
                return false;
            }

            if (!byte.TryParse(match.Groups["B"].Value, out var octetB))
            {
                return false;
            }

            if (!byte.TryParse(match.Groups["C"].Value, out var octetC))
            {
                return false;
            }

            if (!byte.TryParse(match.Groups["D"].Value, out var octetD))
            {
                return false;
            }

            var octets = new[] {octetD, octetC, octetB, octetA};

            try
            {
                parsedValue = BitConverter.ToUInt32(octets, 0);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}