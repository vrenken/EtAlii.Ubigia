namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Runtime.CompilerServices;
    using EtAlii.xTechnology.Hosting;

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

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status == HostStatus.Running;
        }

    }
}