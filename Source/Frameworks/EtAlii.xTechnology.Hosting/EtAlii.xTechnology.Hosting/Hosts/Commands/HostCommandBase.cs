// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;

    public abstract class HostCommandBase<THost>
        where THost: IHost
    {
        protected THost Host { get; }

        public bool CanExecute { get => _canExecute; protected set => SetCanExecute(value, ref _canExecute); }
        private bool _canExecute;

        public event EventHandler CanExecuteChanged;

        protected HostCommandBase(THost host)
        {
            Host = host;
            Host.PropertyChanged += OnHostPropertyChanged;
        }

        private void SetCanExecute(bool newValue, ref bool canExecute)
        {
            if (canExecute == newValue) return;
            
            canExecute = newValue;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnHostPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Host.State):
                    OnHostStateChanged(Host.State);
                    break;
                case nameof(Host.Status):
                    OnHostStatusChanged(Host.Status);
                    break;
            }
        }

        protected virtual void OnHostStatusChanged(Status[] status)
        {
        }

        protected virtual void OnHostStateChanged(State state)
        {
        }
    }

    public abstract class HostCommandBase : HostCommandBase<IHost>
    {
        protected HostCommandBase(IHost host) 
            : base(host)
        {
        }
    }
}
