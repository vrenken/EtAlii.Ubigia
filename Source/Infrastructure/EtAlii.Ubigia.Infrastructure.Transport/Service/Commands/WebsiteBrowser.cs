// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Diagnostics;

    internal class WebsiteBrowser : IWebsiteBrowser
    {
        public void BrowseTo(string relativeAddress)
        {
            var hostAddress = "http://localhost";
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress.TrimStart('/')) { UseShellExecute = true });
        }

    }
}