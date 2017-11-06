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
    }
}