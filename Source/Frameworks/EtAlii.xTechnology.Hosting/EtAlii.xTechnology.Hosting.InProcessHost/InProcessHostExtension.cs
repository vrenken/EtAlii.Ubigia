// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class InProcessHostExtension : IHostExtension
    {
        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
        private readonly HostControl _hostControl;
#pragma warning restore S4487

        public InProcessHostExtension(HostControl hostControl)
        {
            _hostControl = hostControl;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            // Nothing to do here right now...
        }
    }
}
