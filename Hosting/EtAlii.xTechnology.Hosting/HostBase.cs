namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class HostBase
    {
        public HostState State { get => _state; protected set => SetProperty(ref _state, value); }
        private HostState _state;
        public HostStatus[] Status => _status; 
        private HostStatus[] _status = new HostStatus[0];

        public IHostCommand[] Commands => _commands; 
        private IHostCommand[] _commands;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize(IHostCommand[] commands, HostStatus[] status)
        {
            _commands = commands;
            _status = status;

            foreach (var s in _status)
            {
                s.PropertyChanged += OnStatusPropertyChanged;
            }
        }

        private void OnStatusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
        }

        protected void SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!object.Equals((object) storage, (object) newValue))
            {
                storage = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}