﻿namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;

    class StartStorageBrowserCommand : HostCommandBase, IStartStorageBrowserCommand
    {
        private readonly IProcessStarter _processStarter;
        private readonly IInfrastructure _infrastructure;

        public StartStorageBrowserCommand(IProcessStarter processStarter, IInfrastructure infrastructure)
        {
            _processStarter = processStarter;
            _infrastructure = infrastructure;
        }

        public string Name => "Admin/Storage browser";
        public void Execute()
        {
            var storageBrowserPath = "StorageBrowser.exe";
            _processStarter.StartProcess(storageBrowserPath, _infrastructure.Configuration.Address);
        }
    }
}