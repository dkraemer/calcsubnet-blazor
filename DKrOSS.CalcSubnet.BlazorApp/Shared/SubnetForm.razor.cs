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

        private string _inputIpAddressCssClass;

        private byte SelectedPrefixLength
        {
            get => _selectedPrefixLength;
            set
            {
                _selectedPrefixLength = value;
                SubnetMask = SubnetMask.Create(value);
                SubnetMaskChanged.InvokeAsync(SubnetMask);
            }
        }

        private IReadOnlyList<SubnetMask> _subnetMasks;
        private byte _selectedPrefixLength;

        [Parameter]
        public IpAddress IpAddress { get; set; }

        [Parameter]
        public EventCallback<IpAddress> IpAddressChanged { get; set; }

        [Parameter]
        public SubnetMask SubnetMask { get; set; }

        [Parameter]
        public EventCallback<SubnetMask> SubnetMaskChanged { get; set; }

        protected override Task OnInitializedAsync()
        {
            _inputIpAddressCssClass = InvalidInputCssClass;
            SelectedPrefixLength = 24;
            _subnetMasks = SubnetMask.GetAll();
            return base.OnInitializedAsync();
        }

        private async Task OnInputIpAddress(ChangeEventArgs e)
        {
            var isIpAddressValid = e.TryParseDotDecimal(out var parsedIpAddress);
            _inputIpAddressCssClass = isIpAddressValid ? ValidInputCssClass : InvalidInputCssClass;
            IpAddress = isIpAddressValid ? new IpAddress(parsedIpAddress) : null;
            await IpAddressChanged.InvokeAsync(IpAddress);
        }
    }
}