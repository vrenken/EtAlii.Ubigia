namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Admin/Provisioning service/Start";

        public void Execute()
        {
            Host.Start();
        }

        protected override void OnHostStateChanged(HostState state)
        {
            CanExecute = state != HostState.Running;
        }
    }
}