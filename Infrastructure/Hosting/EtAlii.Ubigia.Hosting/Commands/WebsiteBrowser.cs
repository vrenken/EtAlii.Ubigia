namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Functional;

    class WebsiteBrowser : IWebsiteBrowser
    {
        private readonly IInfrastructure _infrastructure;

        public WebsiteBrowser(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void BrowseTo(string relativeAddress)
        {
            var hostAddress = _infrastructure.Configuration.Address.Replace("+", "localhost");
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress) { UseShellExecute = true });
        }

    }
}