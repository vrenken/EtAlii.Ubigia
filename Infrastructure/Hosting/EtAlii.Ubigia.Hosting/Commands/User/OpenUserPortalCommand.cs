namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Runtime.CompilerServices;

    class OpenUserPortalCommand : HostCommandBase, IOpenUserPortalCommand
    {
        public string Name => "User/User portal";

        private readonly IWebsiteBrowser _websiteBrowser;

        public OpenUserPortalCommand(IWebsiteBrowser websiteBrowser)
        {
            _websiteBrowser = websiteBrowser;
        }
        public void Execute()
        {
            _websiteBrowser.BrowseTo("/");
        }
    }
}