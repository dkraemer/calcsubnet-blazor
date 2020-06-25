// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Collections.Generic;
using System.Text;

namespace DKrOSS.CalcSubnet
{
    public class SubnetMask : DotDecimal
    {
        public const byte MaxPrefixLength = 32;

        private SubnetMask(uint? subnetMask, byte prefixLength, byte hostBitCount) : base(subnetMask)
        {
            PrefixLength = prefixLength;
            HostBitCount = hostBitCount;
            HostAddressCount = (ulong) Math.Pow(2, hostBitCount);
            UsableHostAddressCount = HostAddressCount - 2;

            switch (prefixLength)
            {
                case 32:
                    HostAddressRemark = "(Host route)";
                    UsableHostAddressCount = 1;
                    break;
                case 31:
                    HostAddressRemark = "(Point-to-point links (RFC 3021))";
                    UsableHostAddressCount = 2;
                    break;
                case 30:
                    HostAddressRemark = "(Point-to-point links (glue network))";
                    break;
                case 8:
                    HostAddressRemark = "(Largest IANA block allocation)";
                    break;
                default:
                    HostAddressRemark = string.Empty;
                    break;
            }
        }

        public byte PrefixLength { get; }
        public byte HostBitCount { get; }
        public ulong HostAddressCount { get; }
        public ulong UsableHostAddressCount { get; }
        public string HostAddressRemark { get; }

        public override string Dump()
        {
            var hostAddressRemark = HostAddressRemark.Length > 0 ? HostAddressRemark : null;

            var sb = new StringBuilder();
            sb.AppendLine($"Subnet mask: {ToString()}");
            sb.AppendLine($"Prefix length: {PrefixLength}");
            sb.AppendLine($"Host bits: {HostBitCount}");
            sb.AppendLine($"Host addresses: {HostAddressCount}");
            sb.AppendLine($"Host addresses remark: {hostAddressRemark}");
            sb.AppendLine($"Usable host address: {UsableHostAddressCount}");
            return sb.ToString();
        }

        public static IReadOnlyList<SubnetMask> GetAll(
            byte startPrefixLength = 30,
            byte endPrefixLength = 8)
        {
            var subnetMasks = new List<SubnetMask>(MaxPrefixLength);

            var prefixLength = startPrefixLength;
            while (true)
            {
                subnetMasks.Add(Create(prefixLength));
                if (prefixLength == endPrefixLength)
                {
                    break;
                }

                prefixLength--;
            }

            return subnetMasks;
        }

        public static SubnetMask Create(byte prefixLength)
        {
            const string argName = nameof(prefixLength);
            if (prefixLength > MaxPrefixLength)
            {
                throw new ArgumentOutOfRangeException(
                    argName,
                    $"Argument {argName} must be less or equal {MaxPrefixLength}.");
            }

            var hostBitCount = (byte) (MaxPrefixLength - prefixLength);
            var value = prefixLength == 0 ? 0 : uint.MaxValue << hostBitCount;
            return new SubnetMask(value, prefixLength, hostBitCount);
        }
    }
}