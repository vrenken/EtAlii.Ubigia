﻿namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using System.Diagnostics;

    class WebsiteBrowser : IWebsiteBrowser
    {
        public void BrowseTo(string relativeAddress)
        {
            var hostAddress = "http://localhost";
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress.TrimStart('/')) { UseShellExecute = true });
        }

    }
}