// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class InProcessHostExtension : IExtension
    {
        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
        private readonly HostOptions _options;
        private readonly HostControl _hostControl;
#pragma warning restore S4487

        public InProcessHostExtension(HostOptions options, HostControl hostControl)
        {
            _options = options;
            _hostControl = hostControl;
        }

        public void Initialize(IRegisterOnlyContainer container) => container.Register<IHost>(() => new InProcessHost(_options));
    }
}
