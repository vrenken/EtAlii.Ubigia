namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{

    public interface IWebsiteBrowser
    {
        void BrowseTo(string relativeAddress);
    }
}
