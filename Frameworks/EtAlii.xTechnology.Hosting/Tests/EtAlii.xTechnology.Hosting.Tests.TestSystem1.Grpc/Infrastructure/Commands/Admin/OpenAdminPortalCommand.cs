﻿namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class OpenAdminPortalCommand : SystemCommandBase, IOpenAdminPortalCommand
    {
        public string Name => "Admin/Admin portal";

        private readonly IWebsiteBrowser _websiteBrowser;

        public OpenAdminPortalCommand(ISystem system, IWebsiteBrowser websiteBrowser)
            : base(system)
        {
            _websiteBrowser = websiteBrowser;
        }

        public void Execute()
        {
            _websiteBrowser.BrowseTo("/Admin");
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }

    }
}