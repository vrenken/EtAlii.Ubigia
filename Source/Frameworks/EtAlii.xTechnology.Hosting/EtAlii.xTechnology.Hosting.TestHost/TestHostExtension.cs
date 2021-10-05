// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostExtension : IExtension
    {
        private readonly Func<HostOptions, ITestHost> _hostFactory;
        private readonly HostOptions _options;

        public TestHostExtension(HostOptions options, Func<HostOptions, ITestHost> hostFactory)
        {
            _hostFactory = hostFactory;
            _options = options;
        }
        public void Initialize(IRegisterOnlyContainer container)
        {
            container.Register<IHost>(() => _hostFactory(_options));
        }
    }
}
