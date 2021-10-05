// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    internal class CommonHostExtension : IExtension
    {
        private readonly HostOptions _options;

        public CommonHostExtension(HostOptions options)
        {
            _options = options;
        }
        public void Initialize(IRegisterOnlyContainer container)
        {
            new HostScaffolding(_options).Register(container);
        }
    }
}
