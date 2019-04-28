//namespace EtAlii.Ubigia.Provisioning.Hosting
//[
//    using System
//    using System.ComponentModel
//    using EtAlii.xTechnology.Hosting

//    public abstract class HostCommandBase
//    [
//        protected IHost Host [ get; private set; ]

//        public bool CanExecute [ get => _canExecute; protected set => SetCanExecute(value); ]
//        private bool _canExecute

//        public event EventHandler CanExecuteChanged

//        private void SetCanExecute(bool newValue)
//        [
//            if [_canExecute != newValue]
//            [
//                _canExecute = newValue
//                CanExecuteChanged?.Invoke(this, EventArgs.Empty)
//            ]
//        ]
//        public void Initialize(IHost host)
//        [
//            if [Host != null]
//            [
//                Host.PropertyChanged -= OnHostPropertyChanged
//            ]
//            Host = host
//            if [Host != null]
//            [
//                Host.PropertyChanged += OnHostPropertyChanged
//            ]
//        ]
//        private void OnHostPropertyChanged(object sender, PropertyChangedEventArgs e)
//        [
//            switch [e.PropertyName]
//            [
//                case nameof(Host.State):
//                    OnHostStateChanged(Host.State)
//                    break
//                case nameof(Host.Status):
//                    OnHostStatusChanged(Host.Status)
//                    break
//            ]
//        ]
//        protected virtual void OnHostStatusChanged(Status[] hostStatus)
//        [
//        ]
//        protected virtual void OnHostStateChanged(State state)
//        [
//        ]
//    ]
//]