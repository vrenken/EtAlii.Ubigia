namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{

    public interface IWebsiteBrowser
    {
        void BrowseTo(string relativeAddress);
    }
}
