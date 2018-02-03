//namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
//{
//    using System;
//    using System.IO;
//    using EtAlii.Ubigia.Infrastructure.Functional;
//    using EtAlii.xTechnology.Hosting;

//    class StartStorageBrowserCommand : HostCommandBase, IStartStorageBrowserCommand
//    {
//        private readonly IProcessStarter _processStarter;
//        private readonly IInfrastructure _infrastructure;

//        public StartStorageBrowserCommand(IHost host, IProcessStarter processStarter, IInfrastructure infrastructure)
//			: base(host)
//        {
//            _processStarter = processStarter;
//            _infrastructure = infrastructure;
//        }

//        public string Name => "Admin/Storage browser";
//        public void Execute()
//        {
//            var uri = new Uri(this.GetType().Assembly.CodeBase);
//            var folder = Path.GetDirectoryName(uri.LocalPath);

//            folder = Path.Combine(folder, System.Diagnostics.Debugger.IsAttached
//                ? "..\\..\\..\\..\\..\\..\\..\\Client\\Windows\\Diagnostics\\EtAlii.Ubigia.Diagnostics.StorageBrowser\\bin\\Debug\\net47"
//                : "");

//            var executable = "EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser.exe";
//            _processStarter.StartProcess(folder, executable, _infrastructure.Configuration.Address);
//        }

//        protected override void OnHostStateChanged(State state)
//        {
//            CanExecute = state == State.Running;
//        }
//    }
//}