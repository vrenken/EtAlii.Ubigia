// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    internal class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Host/Start";

        public StartHostCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            // Backup any previous host properties that need to be remembered.
            //var property = Host.Property

            // Replace the original host by a completely fresh instance.
            var host = Host.Configuration.CreateHost();
            var hostWrapper = Host as HostWrapper;
            hostWrapper?.Replace(host);

            // And restore the previous host properties.
            // Host.Property = property

            Host.Start();

        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state != State.Running;
        }
    }
}
