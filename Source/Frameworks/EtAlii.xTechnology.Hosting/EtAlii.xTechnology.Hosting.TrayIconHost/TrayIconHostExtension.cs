// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Drawing;
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostExtension : IHostExtension
    {
        private readonly Icon _runningIcon;
        private readonly Icon _stoppedIcon;
        private readonly Icon _errorIcon;

        public TrayIconHostExtension(Icon runningIcon, Icon stoppedIcon, Icon errorIcon)
        {
            _runningIcon = runningIcon;
            _stoppedIcon = stoppedIcon;
            _errorIcon = errorIcon;
        }

        public void Register(IRegisterOnlyContainer container)
        {
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
