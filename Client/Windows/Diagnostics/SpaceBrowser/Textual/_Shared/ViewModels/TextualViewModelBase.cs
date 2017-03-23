﻿namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;

    public class TextualViewModelBase : BindableBase
    {
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }
        private string _title;

        public IEnumerable<TextualError> Errors { get { return _errors; } set { SetProperty(ref _errors, value); } }
        private IEnumerable<TextualError> _errors = new TextualError[] { };

        public bool CanExecute { get { return _canExecute; } set { SetProperty(ref _canExecute, value); } }
        private bool _canExecute = false;

        public bool CanStop { get { return _canStop; } set { SetProperty(ref _canStop, value); } }
        private bool _canStop = false;

        public ICommand ClearCommand { get; }

        public ICommand ExecuteCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand StopCommand { get; }

        public TextualViewModelBase()
        {
            ClearCommand = new RelayCommand(Clear, CanClear);
            ExecuteCommand = new RelayCommand(Execute);
            StopCommand = new RelayCommand(Stop);
            PauseCommand = new RelayCommand(Pause);
        }

        protected virtual bool CanClear(object parameter)
        {
            return false;
        }

        protected virtual void Clear(object parameter)
        {
            throw new NotImplementedException();
        }

        protected virtual void Stop(object obj)
        {
            throw new NotImplementedException();
        }

        protected virtual void Execute(object obj)
        {
            throw new NotImplementedException();
        }

        protected virtual void Pause(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
