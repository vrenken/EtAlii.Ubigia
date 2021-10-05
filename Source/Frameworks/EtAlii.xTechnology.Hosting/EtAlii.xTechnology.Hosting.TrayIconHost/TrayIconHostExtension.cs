// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Drawing;
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostExtension : IExtension
    {
        private readonly HostOptions _options;
        private readonly Icon _runningIcon;
        private readonly Icon _stoppedIcon;
        private readonly Icon _errorIcon;

        public TrayIconHostExtension(HostOptions options, Icon runningIcon, Icon stoppedIcon, Icon errorIcon)
        {
            _options = options;
            _runningIcon = runningIcon;
            _stoppedIcon = stoppedIcon;
            _errorIcon = errorIcon;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            container.Register<IHost>(services =>
            {
                var taskbarIcon = services.GetInstance<ITaskbarIcon>();
                return new TrayIconHost(_options, taskbarIcon);
            });
            var scaffoldings = new IScaffolding[]
            {
                new TrayIconHostScaffolding(_runningIcon, _stoppedIcon, _errorIcon),
            };
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
