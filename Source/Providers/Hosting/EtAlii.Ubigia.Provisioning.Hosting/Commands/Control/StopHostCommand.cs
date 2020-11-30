namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Hosting;

    internal class StopHostCommand : HostCommandBase, IStopHostCommand
    {
        public string Name => "Admin/Provisioning service/Stop";

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