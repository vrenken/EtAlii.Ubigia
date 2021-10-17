// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class InProcessHostExtension : IExtension
    {
#pragma warning disable S4487
        // ReSharper disable once NotAccessedField.Local
        private readonly HostOptions _options;
        // ReSharper disable once NotAccessedField.Local
        private readonly HostControl _hostControl;
#pragma warning restore S4487

        public InProcessHostExtension(HostOptions options, HostControl hostControl)
        {
            _options = options;
            _hostControl = hostControl;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            // Nothing to do here (yet).
        }
    }
}
