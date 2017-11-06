namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    class StopHostCommand : HostCommandBase, IStopHostCommand
    {
        public string Name => "Admin/API service/Stop";

        public StopHostCommand()
        {
        }

        public void Execute()
        {
            Host.Stop();
        }
    }
}