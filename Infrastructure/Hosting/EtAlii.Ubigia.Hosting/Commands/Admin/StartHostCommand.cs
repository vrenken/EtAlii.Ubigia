namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Admin/API service/Start";

        public StartHostCommand()
        {
        }

        public void Execute()
        {
            Host.Start();
        }
    }
}