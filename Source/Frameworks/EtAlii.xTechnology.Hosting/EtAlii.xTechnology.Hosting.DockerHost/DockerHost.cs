// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public partial class DockerHost : HostBase
    {
        public DockerHost(IHostOptions options, ISystemManager systemManager)
            : base(options, systemManager)
        {
        }

    }
}
