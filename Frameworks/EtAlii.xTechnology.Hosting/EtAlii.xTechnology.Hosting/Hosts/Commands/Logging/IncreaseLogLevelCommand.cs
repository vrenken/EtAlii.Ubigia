namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Logging;

    internal class IncreaseLogLevelCommand : HostCommandBase<IHost>, IIncreaseLogLevelCommand
    {
        private readonly IHostManager _manager;
        public string Name => $"Host/Increase log output";

        public IncreaseLogLevelCommand(IHost host, IHostManager manager)
            : base(host)
        {
            _manager = manager;
        }

        public void Execute()
        {
            if (_manager.LogLevel != LogLevel.Trace)
            {
                _manager.LogLevel = _manager.LogLevel - 1;
            }
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running && _manager.LogLevel != LogLevel.Trace;
        }

        protected override void OnHostStatusChanged(Status[] status)
        {
            CanExecute = _manager.LogLevel != LogLevel.Trace;
        }
    }
}