namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;

    class OpenAdminPortalCommand : HostCommandBase, IOpenAdminPortalCommand
    {
        public string Name => "Admin/Admin portal";

        private readonly IWebsiteBrowser _websiteBrowser;

        public OpenAdminPortalCommand(IWebsiteBrowser websiteBrowser)
        {
            _websiteBrowser = websiteBrowser;
        }

        public void Execute()
        {
            _websiteBrowser.BrowseTo("/Admin");
        }

    }
}