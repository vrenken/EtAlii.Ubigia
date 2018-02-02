namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Admin/Provisioning service/Start";

	    public StartHostCommand(IHost host) : base(host)
	    {
	    }

		public void Execute()
        {
            Host.Start();
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state != State.Running;
        }
    }
}