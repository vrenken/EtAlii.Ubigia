namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
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

        protected override void OnHostStateChanged(HostState state)
        {
            CanExecute = state == HostState.Running || state == HostState.Stopped;
        }
    }
}