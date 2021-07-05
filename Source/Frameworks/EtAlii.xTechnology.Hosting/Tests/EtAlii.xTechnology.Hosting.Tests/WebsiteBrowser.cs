// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests
{
    using System.Diagnostics;

    internal class WebsiteBrowser : IWebsiteBrowser
    {
        public void BrowseTo(string relativeAddress)
        {
            var hostAddress = "https://localhost";
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress.TrimStart('/')) { UseShellExecute = true });
        }

    }
}
