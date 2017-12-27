namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{

    public interface IWebsiteBrowser
    {
        void BrowseTo(string relativeAddress);
    }
}
