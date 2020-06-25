// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Runtime.CompilerServices;
using DKrOSS.CalcSubnet.Extensions;

namespace DKrOSS.CalcSubnet
{
    public class DotDecimal : IDumpable, IEquatable<DotDecimal>
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

        public bool Equals(DotDecimal other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is DotDecimal other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(DotDecimal left, DotDecimal right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DotDecimal left, DotDecimal right)
        {
            return !Equals(left, right);
        }
    }
}