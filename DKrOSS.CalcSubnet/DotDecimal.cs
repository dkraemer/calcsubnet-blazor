// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System.Runtime.CompilerServices;
using DKrOSS.CalcSubnet.Extensions;

namespace DKrOSS.CalcSubnet
{
    public class DotDecimal : IDumpable
    {
        private readonly string _dumpLabel;

        public DotDecimal(uint? value, [CallerMemberName] string dumpLabel = "")
        {
            Value = value;
            _dumpLabel = dumpLabel;
        }

        public uint? Value { get; }

        public override string ToString()
        {
            return Value?.ToDotDecimalString();
        }

        public static implicit operator DotDecimal(uint? value)
        {
            return new DotDecimal(value);
        }

        public static implicit operator uint?(DotDecimal obj)
        {
            return obj.Value;
        }

        public virtual string Dump()
        {
            return $"{_dumpLabel}: {ToString()}";
        }
    }
}