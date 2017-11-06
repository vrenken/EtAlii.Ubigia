namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    class StartSpaceBrowserCommand : HostCommandBase, IStartSpaceBrowserCommand
    {
        private readonly IProcessStarter _processStarter;
        private readonly IInfrastructure _infrastructure;

        public StartSpaceBrowserCommand(IProcessStarter processStarter, IInfrastructure infrastructure)
        {
            _processStarter = processStarter;
            _infrastructure = infrastructure;
        }

        public string Name => "Admin/Space browser";

        public void Execute()
        {
            var spaceBrowserPath = "SpaceBrowser.exe";
            _processStarter.StartProcess(spaceBrowserPath, _infrastructure.Configuration.Address);
        }

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status == HostStatus.Running;
        }
    }
}