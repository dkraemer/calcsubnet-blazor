// Copyright (c) 2020, Daniel Kraemer
// All rights reserved.
// Licensed under BSD-3-clause (https://github.com/dkraemer/calcsubnet/blob/master/LICENSE)

using System.Net.Http.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace DKrOSS.CalcSubnet.BlazorApp.Shared
{
    [UsedImplicitly]
    public partial class VersionBadge
    {
        private Manifest _manifest;

        protected override async Task OnInitializedAsync()
        {
            _manifest = await Http.GetFromJsonAsync<Manifest>("manifest.json");
            await base.OnInitializedAsync();
        }

        [UsedImplicitly]
        private class Manifest
        {
            public string Version { get; set; }
        }
    }
}