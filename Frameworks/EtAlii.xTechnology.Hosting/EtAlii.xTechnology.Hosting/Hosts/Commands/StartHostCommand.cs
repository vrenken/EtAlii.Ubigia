namespace EtAlii.xTechnology.Hosting
{
    internal class StartHostCommand : HostCommandBase, IStartHostCommand
    {
        public string Name => "Host/Start";

        public StartHostCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            // Replace the original host by a completely fresh instance.
            var oldHostManager = ((IConfigurableHost)Host).Manager;
            var createHost = Host.Configuration.CreateHost;
            var host = createHost();
            var hostWrapper = Host as HostWrapper; 
            hostWrapper?.Replace(host);

            var newHostManager = ((IConfigurableHost)host).Manager;
            newHostManager.LogLevel = oldHostManager.LogLevel;
            newHostManager.ShouldOutputLog = oldHostManager.ShouldOutputLog;
            
            Host.Start();

        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state != State.Running;
        }
    }
}