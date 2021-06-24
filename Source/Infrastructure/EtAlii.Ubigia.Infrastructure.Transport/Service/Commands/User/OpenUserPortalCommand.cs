// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    internal class OpenUserPortalCommand : SystemCommandBase, IOpenUserPortalCommand
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