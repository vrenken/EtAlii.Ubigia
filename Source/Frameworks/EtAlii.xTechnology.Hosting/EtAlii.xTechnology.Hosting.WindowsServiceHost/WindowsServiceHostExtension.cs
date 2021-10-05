// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class WindowsServiceHostExtension : IExtension
    {
        private readonly HostOptions _options;

        public WindowsServiceHostExtension(HostOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container) => container.Register<IHost>(() => new WindowsServiceHost(_options));
    }
}
