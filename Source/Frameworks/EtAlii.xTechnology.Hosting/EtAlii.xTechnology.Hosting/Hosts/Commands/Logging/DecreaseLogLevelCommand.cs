namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Logging;

    internal class DecreaseLogLevelCommand : HostCommandBase<IHost>, IDecreaseLogLevelCommand
    {
        private readonly IHostManager _manager;
        public string Name => $"Host/Decrease log output";

        public DecreaseLogLevelCommand(IHost host, IHostManager manager)
            : base(host)
        {
            _manager = manager;
        }

        public void Execute()
        {
            if (_manager.LogLevel != LogLevel.Critical)
            {
                _manager.LogLevel = _manager.LogLevel + 1;
            }
        }

        protected override void OnHostStateChanged(State state)
        {
            CanExecute = state == State.Running && _manager.LogLevel != LogLevel.Critical;
        }

        protected override void OnHostStatusChanged(Status[] status)
        {
            CanExecute = _manager.LogLevel != LogLevel.Critical;
        }
    }
}