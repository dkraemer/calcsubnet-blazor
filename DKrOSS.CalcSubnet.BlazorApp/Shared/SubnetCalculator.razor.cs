// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DKrOSS.CalcSubnet.BlazorApp.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class SubnetCalculator
    {
        private const string ValidInputCssClass = "is-valid";
        private const string InvalidInputCssClass = "is-invalid";

        private string _inputIpAddressCssClass;

        private IReadOnlyList<SubnetMask> _subnetMasks;
        private byte _selectedPrefixLength;

        private IpAddress _ipAddress;
        private SubnetMask _subnetMask;
        private bool _isButtonDisabled = true;

        public byte SelectedPrefixLength
        {
            get => _selectedPrefixLength;
            set
            {
                _selectedPrefixLength = value;
                SubnetMask = SubnetMask.Create(value);
                //SubnetMaskChanged.InvokeAsync(SubnetMask);
            }
        }

        [Parameter]
        public IpAddress IpAddress
        {
            get => _ipAddress;
            set
            {
                if (value == _ipAddress)
                {
                    return;
                }

                _ipAddress = value;
                MakeSubnetInfo();
            }
        }

        [Parameter]
        public SubnetMask SubnetMask
        {
            get => _subnetMask;
            set
            {
                if (value == _subnetMask)
                {
                    return;
                }

                _subnetMask = value;
                MakeSubnetInfo();
            }
        }

        [Parameter]
        public SubnetInfo SubnetInfo { get; set; }

        //[Parameter]
        //public EventCallback<IpAddress> IpAddressChanged { get; set; }

        //[Parameter]
        //public EventCallback<SubnetMask> SubnetMaskChanged { get; set; }

        //[Parameter]
        //public EventCallback<SubnetInfo> SubnetInfoChanged { get; set; }

        //[Parameter]
        //public EventCallback<MouseEventArgs> OnClickButtonGenerateList { get; set; }

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
            //await IpAddressChanged.InvokeAsync(IpAddress);
        }

        private async Task OnClickButtonGenerateList(MouseEventArgs e)
        {

        }


        private void MakeSubnetInfo()
        {
            try
            {
                SubnetInfo = new SubnetInfo(IpAddress, SubnetMask);
                _isButtonDisabled = false;
            }
            catch (Exception)
            {
                SubnetInfo = null;
                _isButtonDisabled = true;
            }

            //if (SubnetInfoChanged.HasDelegate)
            //{
            //    SubnetInfoChanged.InvokeAsync(SubnetInfo);
            //}
        }
    }
}