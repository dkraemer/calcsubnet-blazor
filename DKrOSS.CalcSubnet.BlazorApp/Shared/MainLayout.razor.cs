// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    public partial class MainLayout
    {
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            JsRuntime.InvokeVoidAsync("enableToolTips");
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}