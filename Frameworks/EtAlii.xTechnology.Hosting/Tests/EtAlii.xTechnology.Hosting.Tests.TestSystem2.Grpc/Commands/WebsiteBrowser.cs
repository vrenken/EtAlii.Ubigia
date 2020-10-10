namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Grpc
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