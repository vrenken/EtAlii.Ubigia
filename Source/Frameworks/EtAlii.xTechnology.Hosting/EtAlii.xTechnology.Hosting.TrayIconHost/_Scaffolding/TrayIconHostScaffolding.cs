namespace EtAlii.xTechnology.Hosting
{
    using System.Drawing;
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostScaffolding : IScaffolding
    {
        private readonly Icon _runningIcon;
        private readonly Icon _stoppedIcon;
        private readonly Icon _errorIcon;

        public TrayIconHostScaffolding(Icon runningIcon, Icon stoppedIcon, Icon errorIcon)
        {
            _runningIcon = runningIcon;
            _stoppedIcon = stoppedIcon;
            _errorIcon = errorIcon;
        }

        public void Register(Container container)
        {
            container.Register<IHostCommandsConverter, HostCommandsConverter>();
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<IHost>(host =>
            {
                container.GetInstance<ITaskbarIconViewModel>().Initialize((ITrayIconHost)host, _runningIcon, _stoppedIcon, _errorIcon);
            });
        }
    }
}