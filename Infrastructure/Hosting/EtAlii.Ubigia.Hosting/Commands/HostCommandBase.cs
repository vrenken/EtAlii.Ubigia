namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.xTechnology.Hosting;

    public abstract class HostCommandBase
    {
        protected IHost Host { get; private set; }

        public bool CanExecute { get => _canExecute; protected set => SetCanExecute(value); }
        private bool _canExecute;

        public event EventHandler CanExecuteChanged;

        private void SetCanExecute(bool newValue)
        {
            if (_canExecute != newValue)
            {
                _canExecute = newValue;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Initialize(IHost host)
        {
            if (Host != null)
            {
                Host.StatusChanged -= OnHostStatusChanged;
            }
            Host = host;
            if (Host != null)
            {
                Host.StatusChanged += OnHostStatusChanged;
            }
        }

        protected virtual void OnHostStatusChanged(HostStatus status)
        {
        }
    }
}
