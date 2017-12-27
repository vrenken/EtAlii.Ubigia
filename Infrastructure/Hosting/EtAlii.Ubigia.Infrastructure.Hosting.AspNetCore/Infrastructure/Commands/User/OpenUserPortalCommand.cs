namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using System;
    using System.Runtime.CompilerServices;
    using EtAlii.xTechnology.Hosting;

    class OpenUserPortalCommand : SystemCommandBase, IOpenUserPortalCommand
    {
        public string Name => "User/User portal";

        private readonly IWebsiteBrowser _websiteBrowser;

        public OpenUserPortalCommand(ISystem system, IWebsiteBrowser websiteBrowser)
            : base(system)
        {
            _websiteBrowser = websiteBrowser;
        }
        public void Execute()
        {
            _websiteBrowser.BrowseTo("/");
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }

    }
}