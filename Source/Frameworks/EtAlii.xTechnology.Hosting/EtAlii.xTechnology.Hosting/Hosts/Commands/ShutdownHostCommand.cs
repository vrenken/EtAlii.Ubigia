// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    internal class ShutdownHostCommand : HostCommandBase, IShutdownHostCommand
    {
        public string Name => "Shutdown";

        public ShutdownHostCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            Host.Shutdown();
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running || state == State.Stopped;
        }
    }
}