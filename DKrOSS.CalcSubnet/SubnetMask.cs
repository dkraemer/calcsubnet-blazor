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
        public ulong HostCount { get; }

        public SubnetMask(byte networkBitCount)
        {
            const string argName = nameof(networkBitCount);
            if (networkBitCount > MaxNetworkBitCount)
            {
                throw new ArgumentOutOfRangeException(argName,$"Argument {argName} must be less or equal {MaxNetworkBitCount}.");
            }

            NetwortBitCount = networkBitCount;
            HostBitCount = (byte) (ValueBitCount - networkBitCount);
            HostCount = (ulong) Math.Pow(2, HostBitCount);
            Value = ulong.MaxValue << HostBitCount;
        }

        public override string Dump()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.Dump());
            sb.AppendLine($"NetworkBitCount: {NetwortBitCount}");
            sb.AppendLine($"HostBitCount: {HostBitCount}");
            sb.AppendLine($"HostCount: {HostCount}");
            return sb.ToString();
        }

        public static IReadOnlyList<SubnetMask> AllSubnetMasks()
        {
            var subnetMasks = new List<SubnetMask>(MaxNetworkBitCount);

            var networkBitCount = MaxNetworkBitCount;
            while (true)
            {
                subnetMasks.Add(new SubnetMask(networkBitCount));
                if (networkBitCount == 0)
                {
                    break;
                }

                networkBitCount--;
            }

            return subnetMasks;
        }
    }
}
