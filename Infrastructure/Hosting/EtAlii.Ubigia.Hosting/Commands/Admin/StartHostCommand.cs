namespace EtAlii.Ubigia.Infrastructure.Hosting
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

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status != HostStatus.Running;
        }
    }
}