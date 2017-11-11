namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Admin/API service/Start";

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