// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Provisioning
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class OpenProvisioningPortalCommand : SystemCommandBase, IOpenProvisioningPortalCommand
    {
        public string Name => "Provisioning/Provisioning portal";

        private readonly IWebsiteBrowser _websiteBrowser;

        public OpenProvisioningPortalCommand(ISystem system, IWebsiteBrowser websiteBrowser)
            : base(system)
        {
            _websiteBrowser = websiteBrowser;
        }

        public void Execute()
        {
            _websiteBrowser.BrowseTo("/Provisioning");
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }

    }
}