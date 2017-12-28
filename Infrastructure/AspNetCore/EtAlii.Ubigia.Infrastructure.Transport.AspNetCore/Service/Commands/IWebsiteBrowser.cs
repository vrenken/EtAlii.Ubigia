namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{

    public interface IWebsiteBrowser
    {
        void BrowseTo(string relativeAddress);
    }
}
