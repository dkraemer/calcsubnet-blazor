// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using Microsoft.AspNetCore.Components.Web;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class MainLayout
    {
#pragma warning disable 169
        private IpAddress _ipAddress;
        private SubnetMask _subnetMask;
        private SubnetInfo _subnetInfo;
        private string _message;
#pragma warning restore 169

        private void ShowMessage(MouseEventArgs e)
        {
            _message = $"Message! X:{e.ScreenX} Y:{e.ScreenY}";
        }
    }
}