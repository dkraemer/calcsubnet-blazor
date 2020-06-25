// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using Microsoft.AspNetCore.Components;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class SubnetInfoDisplay
    {
        [Parameter]
        public uint? IpAddress
        {
            get => _ipAddress;
            set
            {
                VisibilityCss = value != null ? VisibleCssClass : InvisibleCssClass;
                _ipAddress = value;
            }
        }

        [Parameter]
        public SubnetMask SubnetMask { get; set; }

        private const string VisibleCssClass = "visible border-primary";
        private const string InvisibleCssClass = "invisible";

        private uint? _ipAddress;
        private string VisibilityCss { get; set; } = InvisibleCssClass;
    }
}