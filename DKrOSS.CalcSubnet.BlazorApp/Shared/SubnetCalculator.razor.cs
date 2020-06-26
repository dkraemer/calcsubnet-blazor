// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DKrOSS.CalcSubnet.BlazorApp.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    [UsedImplicitly]
    public partial class SubnetCalculator
    {
        private const string ValidInputCssClass = "is-valid";
        private const string InvalidInputCssClass = "is-invalid";

        private string _inputIpAddressCssClass;
        private IReadOnlyList<SubnetMask> _subnetMasks;
        private byte _selectedPrefixLength;
        private IpAddress _ipAddress;
        private SubnetMask _subnetMask;

        // Prevent warning CS0414: The field 'SubnetCalculator._isButtonDisabled' is assigned but its value is never used.
        // _isButtonDisabled is used in button sid-btn-generate-list.
#pragma warning disable 414
        private bool _isButtonDisabled;
#pragma warning restore 414

        private byte SelectedPrefixLength
        {
            get => _selectedPrefixLength;
            set
            {
                _selectedPrefixLength = value;
                SubnetMask = SubnetMask.Create(value);
            }
        }

        private IpAddress IpAddress
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

        private SubnetMask SubnetMask
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

        private SubnetInfo SubnetInfo { get; set; }

        protected override Task OnInitializedAsync()
        {
            _inputIpAddressCssClass = InvalidInputCssClass;
            SelectedPrefixLength = 24;
            _subnetMasks = SubnetMask.GetAll();
            return base.OnInitializedAsync();
        }

        private void OnInputIpAddress(ChangeEventArgs e)
        {
            var isIpAddressValid = e.TryParseDotDecimal(out var parsedIpAddress);
            _inputIpAddressCssClass = isIpAddressValid ? ValidInputCssClass : InvalidInputCssClass;
            IpAddress = isIpAddressValid ? new IpAddress(parsedIpAddress) : null;
        }

        private void OnClickButtonGenerateList(MouseEventArgs e)
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
        }
    }
}