namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using System.Diagnostics;

    class WebsiteBrowser : IWebsiteBrowser
    {
        public WebsiteBrowser()
        {
        }

        public void BrowseTo(string relativeAddress)
        {
            var hostAddress = "http://localhost";
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress.TrimStart('/')) { UseShellExecute = true });
        }

    }
}