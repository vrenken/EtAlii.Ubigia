namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using EtAlii.xTechnology.Hosting;

    class ShutdownHostCommand : HostCommandBase, IShutdownHostCommand
    {
        public string Name => "Shutdown";

	    public ShutdownHostCommand(IHost host) : base(host)
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