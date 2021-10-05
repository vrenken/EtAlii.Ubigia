// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class DockerHostExtension : IExtension
    {
        private readonly HostOptions _options;

        public DockerHostExtension(HostOptions options)
        {
            _options = options;
        }
        public void Initialize(IRegisterOnlyContainer container) => container.Register<IHost>(() => new DockerHost(_options));
    }
}
