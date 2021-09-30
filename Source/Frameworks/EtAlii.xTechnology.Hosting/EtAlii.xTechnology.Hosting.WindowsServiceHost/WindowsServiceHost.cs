// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public abstract partial class WindowsServiceHost : HostBase
    {
        protected WindowsServiceHost(IHostOptions options, IHostServicesFactory hostServicesFactory)
            : base(options, hostServicesFactory)
        {
        }
    }
}
