// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class SubnetInfoDisplay
    {
        private IpAddress _ipAddress;
        private SubnetMask _subnetMask;

        [Parameter]
        public IpAddress IpAddress
        {
            get => _ipAddress;
            set
            {
                if(value == _ipAddress)
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
                if(value == _subnetMask)
                {
                    return;
                }

                _subnetMask = value;
                MakeSubnetInfo();
            }
        }

        [Parameter]
        public SubnetInfo SubnetInfo { get; set; }

        [Parameter]
        public EventCallback<IpAddress> IpAddressChanged { get; set; }

        [Parameter]
        public EventCallback<SubnetMask> SubnetMaskChanged { get; set; }

        [Parameter]
        public EventCallback<SubnetInfo> SubnetInfoChanged { get; set; }

        private void MakeSubnetInfo()
        {
            try
            {
                SubnetInfo = new SubnetInfo(IpAddress, SubnetMask);
            }
            catch(Exception)
            {
                SubnetInfo = null;
            }

            if (SubnetInfoChanged.HasDelegate)
            {
                   SubnetInfoChanged.InvokeAsync(SubnetInfo);
            }
        }
    }
}