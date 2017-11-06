namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

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

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status == HostStatus.Running;
        }

    }
}