// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InProcessHost : HostBase
    {
        protected InProcessHost(IHostOptions options, ISystemManager systemManager)
            : base(options, systemManager)
        {
        }
    }
}