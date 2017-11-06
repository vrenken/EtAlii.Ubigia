namespace EtAlii.xTechnology.Hosting
{
    using System;

    public abstract class HostBase
    {
        public HostStatus Status { get => _status; protected set => SetStatus(value); }
        private HostStatus _status;
        public IHostCommand[] Commands => _commands; 
        private IHostCommand[] _commands;

        public event Action<HostStatus> StatusChanged;

        private void SetStatus(HostStatus newValue)
        {
            if (_status != newValue)
            {
                _status = newValue;
                StatusChanged?.Invoke(_status);
            }
        }

        public void Initialize(IHostCommand[] commands)
        {
            _commands = commands;
        }
    }
}