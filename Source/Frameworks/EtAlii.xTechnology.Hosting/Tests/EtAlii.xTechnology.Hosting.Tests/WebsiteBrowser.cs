﻿namespace EtAlii.xTechnology.Hosting.Tests
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