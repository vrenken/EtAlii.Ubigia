namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
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

        protected override void OnHostStateChanged(HostState state)
        {
            CanExecute = state == HostState.Running;
        }

    }
}