// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public class HostControl
    {
        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
        private readonly IHost _host;
#pragma warning restore S4487

        public HostControl(IHost host)
        {
            _host = host;
        }

        public void StartHost()
        {
            // Nothing to do here right now.
        }
        public void StopHost()
        {
            // Nothing to do here right now.
        }
    }
}