// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;

    public abstract class SystemCommandBase
    {
        protected ISystem System { get; }

        public bool CanExecute { get => _canExecute; protected set => SetCanExecute(value, ref _canExecute); }
        private bool _canExecute;

        public event EventHandler CanExecuteChanged;

        protected SystemCommandBase(ISystem system)
        {
            System = system;
            System.PropertyChanged += OnSystemPropertyChanged;
        }

        private void SetCanExecute(bool newValue, ref bool canExecute)
        {
            if (canExecute == newValue) return;
            canExecute = newValue;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnSystemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(System.State):
                    OnSystemStateChanged(System.State);
                    break;
                case nameof(System.Status):
                    OnSystemStatusChanged(System.Status);
                    break;
            }
        }

        protected virtual void OnSystemStatusChanged(Status status)
        {
        }

        protected virtual void OnSystemStateChanged(State state)
        {
        }
    }
}
