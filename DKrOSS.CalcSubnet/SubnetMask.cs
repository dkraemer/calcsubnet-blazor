using System;
using System.Collections.Generic;
using System.Text;

namespace DKrOSS.CalcSubnet
{
    public interface IDumpable
    {
        string Dump();
    }

    public abstract class OctetValue : IDumpable
    {
        public const byte ValueBitCount = 32;

        private string _octetString;
        private ulong _value;

        public ulong Value
        {
            get => _value;
            protected set
            {
                _value = value;
                var bytes = BitConverter.GetBytes(value);
                _octetString = $"{bytes[3]}.{bytes[2]}.{bytes[1]}.{bytes[0]}";
            }
        }

        public override string ToString()
        {
            return _octetString;
        }

        public virtual string Dump()
        {
            return $"Value: {_octetString}";
        }
    }

    public class SubnetMask : OctetValue
    {
        public const byte MaxNetworkBitCount = 32;

        public byte NetwortBitCount { get; }
        public byte HostBitCount { get; }
        public ulong HostAddressCount { get; }
        public ulong UsableHostAddressCount { get; }
        public string HostAddressRemark { get; }

        public SubnetMask(byte networkBitCount)
        {
            const string argName = nameof(networkBitCount);
            if (networkBitCount > MaxNetworkBitCount)
            {
                throw new ArgumentOutOfRangeException(argName,$"Argument {argName} must be less or equal {MaxNetworkBitCount}.");
            }

            NetwortBitCount = networkBitCount;
            HostBitCount = (byte) (ValueBitCount - networkBitCount);
            Value = ulong.MaxValue << HostBitCount;
            HostAddressCount = (ulong) Math.Pow(2, HostBitCount);
            UsableHostAddressCount = HostAddressCount - 2;

            switch (networkBitCount)
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

        public override string Dump()
        {
            var hostAddressRemark = HostAddressRemark.Length > 0 ? HostAddressRemark : "<unset>";

            var sb = new StringBuilder();
            sb.AppendLine(base.Dump());
            sb.AppendLine($"NetworkBitCount: {NetwortBitCount}");
            sb.AppendLine($"HostBitCount: {HostBitCount}");
            sb.AppendLine($"HostAddressCount: {HostAddressCount}");
            sb.AppendLine($"HostAddressRemark: {hostAddressRemark}");
            sb.AppendLine($"UsableHostAddressCount: {UsableHostAddressCount}");
            return sb.ToString();
        }

        public static IReadOnlyList<SubnetMask> AllSubnetMasks(byte startNetworkBitCount = 30, byte endNetworkBitCount = 8)
        {
            var subnetMasks = new List<SubnetMask>(MaxNetworkBitCount);

            var networkBitCount = startNetworkBitCount;
            while (true)
            {
                subnetMasks.Add(new SubnetMask(networkBitCount));
                if (networkBitCount == endNetworkBitCount)
                {
                    break;
                }

                networkBitCount--;
            }

            return subnetMasks;
        }
    }
}
