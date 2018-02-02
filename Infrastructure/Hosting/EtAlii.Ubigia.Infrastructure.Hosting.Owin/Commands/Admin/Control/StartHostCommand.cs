namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Admin/API service/Start";

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