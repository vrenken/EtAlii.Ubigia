namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{

    public interface IWebsiteBrowser
    {
        void BrowseTo(string relativeAddress);
    }
}
