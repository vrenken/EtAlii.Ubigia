namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StopHostCommand : HostCommandBase, IStopHostCommand
    {
        public string Name => "Admin/Provisioning service/Stop";

        public void Execute()
        {
            Host.Stop();
        }

        protected override void OnHostStateChanged(HostState state)
        {
            CanExecute = state == HostState.Running;
        }
    }
}