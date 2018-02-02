namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using System.IO;
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
            var uri = new Uri(this.GetType().Assembly.CodeBase);
            var folder = Path.GetDirectoryName(uri.LocalPath);

            folder = Path.Combine(folder, System.Diagnostics.Debugger.IsAttached
                ? "..\\..\\..\\..\\..\\..\\..\\Client\\Windows\\Diagnostics\\SpaceBrowser\\bin\\Debug\\net47"
                : "");

            var executable = "EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.exe";
            _processStarter.StartProcess(folder, executable, _infrastructure.Configuration.Address);
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running;
        }
    }
}