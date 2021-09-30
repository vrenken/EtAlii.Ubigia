// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public abstract partial class ConsoleHost : HostBase
    {
        protected ConsoleHost(IHostOptions options, IHostServicesFactory hostServicesFactory)
            : base(options, hostServicesFactory)
        {
        }

    }
}
