namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Hosting;

    class ShutdownHostCommand : HostCommandBase, IShutdownHostCommand
    {
        public string Name => "Shutdown";

        public ShutdownHostCommand()
        {
        }
        public void Execute()
        {
            Host.Shutdown();
        }

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status == HostStatus.Running || status == HostStatus.Stopped;
        }
    }
}