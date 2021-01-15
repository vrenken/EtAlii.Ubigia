namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Logging;

    internal class IncreaseLogLevelCommand : HostCommandBase<IHost>, IIncreaseLogLevelCommand
    {
        public string Name => $"Host/Increase log output";

        public IncreaseLogLevelCommand(IHost host)
            : base(host)
        {
        }

        public void Execute()
        {
            if (Host.LogLevel != LogLevel.Trace)
            {
                Host.LogLevel = Host.LogLevel - 1;
            }
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running && Host.LogLevel != LogLevel.Trace;
        }

        protected override void OnHostStatusChanged(Status[] status)
        {
            CanExecute = Host.LogLevel != LogLevel.Trace;
        }
    }
}
