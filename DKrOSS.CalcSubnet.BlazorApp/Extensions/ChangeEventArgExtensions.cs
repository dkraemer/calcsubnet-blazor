// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using DKrOSS.CalcSubnet.Extensions;
using Microsoft.AspNetCore.Components;

namespace DKrOSS.CalcSubnet.BlazorApp.Extensions
{
    public static class ChangeEventArgExtensions
    {
        public static bool TryParseDotDecimal(this ChangeEventArgs e, out uint? parsedValue)
        {
            parsedValue = null;
            return e?.Value != null && e.Value.ToString().TryParseDotDecimal(out parsedValue);
        }
    }
}