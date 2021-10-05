// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class ConsoleHostExtension : IExtension
    {
        private readonly HostOptions _options;

        public ConsoleHostExtension(HostOptions options)
        {
            _options = options;
        }
        public void Initialize(IRegisterOnlyContainer container) => container.Register<IHost>(() => new ConsoleHost(_options));
    }
}
