// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public abstract partial class InProcessHost : HostBase
    {
        protected InProcessHost(IHostOptions options, IHostServicesFactory hostServicesFactory)
            : base(options, hostServicesFactory)
        {
        }
    }
}
