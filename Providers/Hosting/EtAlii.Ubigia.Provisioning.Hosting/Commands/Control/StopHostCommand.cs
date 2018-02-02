﻿namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StopHostCommand : HostCommandBase, IStopHostCommand
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