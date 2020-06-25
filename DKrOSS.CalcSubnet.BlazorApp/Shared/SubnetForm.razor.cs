// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System.Collections.Generic;
using System.Threading.Tasks;
using DKrOSS.CalcSubnet.BlazorApp.Extensions;
using Microsoft.AspNetCore.Components;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class SubnetForm
    {
        private const string ValidInputCssClass = "is-valid";
        private const string InvalidInputCssClass = "is-invalid";

        private string InputIpAddressCss { get; set; }
        private byte SelectedNetworkBitCount { get; set; }
        private IReadOnlyList<SubnetMask> SubnetMasks { get; set; }

        [Parameter]
        public uint? IpAddress { get; set; }

        [Parameter]
        public EventCallback<uint?> IpAddressChanged { get; set; }

        protected override Task OnInitializedAsync()
        {
            InputIpAddressCss = InvalidInputCssClass;
            SelectedNetworkBitCount = 24;
            SubnetMasks = SubnetMask.GetAll();
            return base.OnInitializedAsync();
        }

        private Task OnInputIpAddress(ChangeEventArgs e)
        {
            var isIpAddressValid = e.TryParseDotDecimal(out var parsedIpAddress);
            IpAddress = parsedIpAddress;

            InputIpAddressCss = !isIpAddressValid ? InvalidInputCssClass : ValidInputCssClass;

            return IpAddressChanged.InvokeAsync(IpAddress);
        }
    }
}
