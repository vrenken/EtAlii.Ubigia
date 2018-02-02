namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using EtAlii.xTechnology.Hosting;

    class StopHostCommand : HostCommandBase, IStopHostCommand
    {
        public string Name => "Admin/API service/Stop";

	    public StopHostCommand(IHost host) : base(host)
	    {
	    }

		public void Execute()
        {
            Host.Stop();
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}